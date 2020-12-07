using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Crowfounding.Areas.Identity;
using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Globalization;
using Crowfounding.Services;
using Amazon.S3;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Crowfounding.BackroundJob;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using MatBlazor;
using Crowfounding.Services.Finances;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Crowfounding
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                //opts.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/InactiveSponsor");
                opts.Password.RequiredLength = 5;   // ����������� �����
                opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
                opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
                opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                opts.Password.RequireDigit = false; // ��������� �� �����

                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.AllowedForNewUsers = true;

                //opts.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Auth:Google");

                    options.ClientId = Configuration["GOOGLE_CLIENT_ID"]; //googleAuthNSection["client_id"];
                    options.ClientSecret = Configuration["GOOGLE_CLIENT_SECRET"]; //googleAuthNSection["client_secret"];
                    //options.ReturnUrlParameter = googleAuthNSection["redirect_uris"];
                    options.CallbackPath = new PathString("/Identity/Account/GoogleLoginCallback");
                })
                // .AddOAuth("VK", "Vkontakte", options =>
                // {
                //     IConfigurationSection vkAuthNSection =
                //         Configuration.GetSection("Auth:VK");
                //     
                //     options.ClientId = vkAuthNSection["client_id"];
                //     options.ClientSecret = vkAuthNSection["client_secret"];
                //     options.ClaimsIssuer = "Vkontakte";
                //     options.CallbackPath = new PathString("/signin-vkontakte");
                //     options.AuthorizationEndpoint = "https://oauth.vk.com/authorise";
                //     options.TokenEndpoint = "https://oauth.vk.com/access_token";
                //     // options.Scope.Add("email");
                //     options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                //     options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                //     options.SaveTokens = true;
                //     options.Events = new OAuthEvents()
                //     {
                //         OnCreatingTicket = context =>
                //         {
                //             context.RunClaimActions(context.TokenResponse.Response.RootElement);
                //             return Task.CompletedTask;
                //         }
                //     };
                // })
                .AddVkontakte(options =>
                {
                    IConfigurationSection vkAuthNSection =
                        Configuration.GetSection("Auth:VK");

                    options.ClientId = Configuration["VK_CLIENT_ID"];
                    options.ClientSecret = Configuration["VK_CLIENT_SECRET"];
                    // options.CallbackPath = new PathString("/Identity/Account/GoogleLoginCallback");
                    //options.ApiVersion = "5.103";
                    //options.Fields.Add("grant_type=client_credentials");
                });

            services.Configure<EmailSettings>(options =>
            {
                options.MailServer = Configuration["EMAIL_MailServer"];
                options.MailPort = 465;
                options.SenderName = "Confirm mail";
                options.SenderEmail = Configuration["EMAIL_SenderEmail"];
                options.SenderPassword = Configuration["EMAIL_SenderPassword"];
                options.Password = Configuration["EMAIL_Password"];
            });
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
            services.AddScoped<ThemeService>();
            services.AddHostedService<TimedHostedService>();
            

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options => {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddViewLocalization();// ��������� ����������� �������������;
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            var supportedCultures = new List<CultureInfo> { new CultureInfo("ru"), new CultureInfo("en") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(new CultureInfo("en"), new CultureInfo("en"));
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "�";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            services.AddSingleton<IS3Service, S3Service>();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddSingleton<CommentService>();
            services.AddScoped<SingleCommentService>();
            services.AddScoped<IFinanceService, FinanceService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.TopCenter;
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.ShowProgressBar = true;
                config.MaximumOpacity = 95;
                config.ShowTransitionDuration = 500;
                config.VisibleStateDuration = 5000;
                config.HideTransitionDuration = 500;
                config.RequireInteraction = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IS3Service _is3,
            ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IOptions<EmailSettings> emailSettings)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            DefaultDataDatabase.Initialize(context, userManager, roleManager, emailSettings).Wait();
        }
    }
}

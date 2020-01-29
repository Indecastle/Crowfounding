using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Crowfounding.Areas.Identity;
using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Crowfounding.Data;
using System.Globalization;
using Crowfounding.Services;

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

                    options.ClientId = googleAuthNSection["client_id"];
                    options.ClientSecret = googleAuthNSection["client_secret"];
                    //options.ReturnUrlParameter = googleAuthNSection["redirect_uris"];
                    options.CallbackPath = new PathString("/Identity/Account/GoogleLoginCallback");
                })
                .AddVkontakte(options =>
                {
                    IConfigurationSection vkAuthNSection =
                        Configuration.GetSection("Auth:VK");

                    options.ClientId = vkAuthNSection["client_id"];
                    options.ClientSecret = vkAuthNSection["client_secret"];
                    //options.CallbackPath = new PathString("/VKLoginCallback");
                    //options.ApiVersion = "5.103";
                    //options.Fields.Add("grant_type=client_credentials");
                });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<ThemeService>();
            services.AddHttpContextAccessor();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            var supportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("ru") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

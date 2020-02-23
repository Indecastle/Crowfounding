using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services
{
    public class ThemeService
    {
        public ApplicationDbContext _db { get; set; }
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public UserManager<User> _userManager { get; set; }
        public IHttpContextAccessor _context { get; set; }

        public ThemeService(ApplicationDbContext db, AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager, IHttpContextAccessor context)
        {
            this._db = db;
            this.AuthenticationStateProvider = authenticationStateProvider;
            this._userManager = userManager;
            this._context = context;
        }

        public HtmlString GetThemeHtml()
        {
            if (_context.HttpContext.Request.Cookies.ContainsKey("Theme"))
            {
                string a = _context.HttpContext.Request.Cookies["Theme"];
                if (Enum.TryParse(a, true, out ThemeType theme))
                    switch (theme)
                    {
                        case ThemeType.Original: return new HtmlString(@"<link href=""css/site.css"" rel=""stylesheet"" />");
                        case ThemeType.Black: return new HtmlString(@"<link href=""css/site-Black.css"" rel=""stylesheet"" />");
                    }
            }

            return new HtmlString(@"<link href=""css/site.css"" rel=""stylesheet"" />");
        }

        public string GetThemeHtml_Identity()
        {
            if (_context.HttpContext.Request.Cookies.ContainsKey("Theme"))
            {
                string a = _context.HttpContext.Request.Cookies["Theme"];
                if (Enum.TryParse(a, true, out ThemeType theme))
                    switch (theme)
                    {
                        case ThemeType.Original: return "navbar-light bg-white";
                        case ThemeType.Black: return "navbar-dark bg-dark";
                    }
            }

            return "navbar-light bg-white";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowfounding.Data;
using Crowfounding.Models;
using Crowfounding.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowfounding
{
    public class SetCultureModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public SetCultureModel(ApplicationDbContext db,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(string culture, string redirectUri)
        {
            if (culture != null && Enum.TryParse(culture, true, out CultureType cultureEnum))
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));

                if (User.Identity.IsAuthenticated)
                {
                    User user = await _userManager.GetUserAsync(User);
                    user.Language = cultureEnum;
                    await _db.SaveChangesAsync();
                    
                }
            }

            return LocalRedirect(redirectUri);
        }
    }
}
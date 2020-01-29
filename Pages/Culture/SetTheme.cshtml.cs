using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowfounding.Data;
using Crowfounding.Models;
using Crowfounding.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowfounding
{
    public class SetThemeModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public SetThemeModel(ApplicationDbContext db,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(string theme, string redirectUri)
        {
            if (theme != null && Enum.TryParse(theme, true, out ThemeType themeEnum))
            {
                HttpContext.Response.Cookies.Append("Theme", theme);

                if (User.Identity.IsAuthenticated)
                {
                    User user = await _userManager.GetUserAsync(User);
                    user.Theme = themeEnum;
                    await _db.SaveChangesAsync();
                }
            }

            return LocalRedirect(redirectUri);
        }
    }
}
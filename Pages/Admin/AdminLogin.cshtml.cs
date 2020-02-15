using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowfounding.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Crowfounding
{
    [Authorize]
    public class AdminLoginModel : PageModel
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AdminLoginModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AdminLoginModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string redirectUri)
        {
            User user = await _userManager.FindByIdAsync(userId);
            _logger.LogInformation("Use Admin login");
            if (user != null)
            {
                await _signInManager.SignInAsync(user, false);
            }

            return LocalRedirect(redirectUri);
        }
    }
}
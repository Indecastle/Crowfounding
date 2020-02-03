using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowfounding
{
    public class DeleteCompanyModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public DeleteCompanyModel(ApplicationDbContext db,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(int companyId, string redirectUri)
        {
            Company company = new Company { Id = companyId };
            _db.Companies.Attach(company);
            _db.Companies.Remove(company);
            _db.SaveChanges();

            return LocalRedirect(redirectUri);
        }
    }
}
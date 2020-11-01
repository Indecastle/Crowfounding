using Crowfounding.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime DataLogin { get; set; }
        public ThemeType Theme { get; set; }
        public CultureType Language { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<Donation> Donations { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}

using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Crowfounding.Services
{
    public class CommentService
    {
        public IServiceProvider Services { get; }
        public List<Company> Companies { get; set; }
        public CommentService(IServiceProvider services)
        {
            Services = services;

            using (var scope = Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Companies = (from ff in _db.Companies.Include(g => g.Comments)
                             select ff).ToList();
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using Crowfounding.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crowfounding.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>()
                .HasOne(a => a.Owner)
                .WithMany(d => d.Companies)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Donation>()
                .HasOne(a => a.Payer)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.UserId);

            builder.Entity<Donation>()
                .HasOne(a => a.Company)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(a => a.CompanyComment)
                .WithMany(d => d.Comments)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ImagesCompany>()
                .HasOne(a => a.Company)
                .WithMany(d => d.CompanyImages)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Rating>()
                .HasOne(a => a.User)
                .WithMany(d => d.Ratings)
                .HasForeignKey(d => d.UserId);

            builder.Entity<Rating>()
                .HasOne(a => a.Company)
                .WithMany(d => d.Ratings)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ImagesCompany> CompanyImages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}

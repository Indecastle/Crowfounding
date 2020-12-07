using System;
using System.Collections.Generic;
using System.Text;
using Crowfounding.Models;
using Crowfounding.Models.Finance;
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

            builder.Entity<User>()
                .Property(t => t.Money)
                .HasColumnType("decimal(18,4)");

            builder.Entity<Company>()
                .HasOne(a => a.Owner)
                .WithMany(d => d.Companies)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Company>()
                .Property(t => t.NeedMoney)
                .HasColumnType("decimal(18,4)");
            builder.Entity<Company>()
                .Property(t => t.CurrentMoney)
                .HasColumnType("decimal(18,4)");

            builder.Entity<Donation>()
                .HasOne(a => a.Payer)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.UserId);
            builder.Entity<Donation>()
                .HasOne(a => a.Company)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Donation>()
                .Property(t => t.MonyDonate)
                .HasColumnType("decimal(18,4)");

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

            builder.Entity<Transaction>()
                .HasOne(a => a.User)
                .WithMany(d => d.Transactions)
                .HasForeignKey(d => d.UserId);
            builder.Entity<Transaction>()
                .HasOne(a => a.Company)
                .WithMany(d => d.Transactions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,4)");

            builder.Entity<TotalDonate>()
                .HasOne(a => a.User)
                .WithMany(d => d.TotalDonates)
                .HasForeignKey(d => d.UserId);
            builder.Entity<TotalDonate>()
                .HasOne(a => a.Company)
                .WithMany(d => d.TotalDonates)
                .HasForeignKey(d => d.CompanyId);
            builder.Entity<TotalDonate>()
                .Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");
            builder.Entity<TotalDonate>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,4)");

            builder.Entity<Bonuse>()
                .HasOne(a => a.Company)
                .WithMany(d => d.Bonuses)
                .HasForeignKey(d => d.CompanyId);
            builder.Entity<Bonuse>()
                .Property(t => t.NeedAmount)
                .HasColumnType("decimal(18,4)");
            
            builder.Entity<UserBonuse>()
                .HasOne(a => a.Bonuse)
                .WithMany(a => a.UserBonuses)
                .HasForeignKey(d => d.BonuseId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserBonuse>()
                .HasOne(a => a.Company)
                .WithMany(a => a.UserBonuses)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserBonuse>()
                .HasOne(a => a.User)
                .WithMany(a => a.UserBonuses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserBonuse>()
                .HasOne(a => a.TotalDonate)
                .WithMany(a => a.UserBonuses)
                .HasForeignKey(d => d.TotalDonateId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ImagesCompany> CompanyImages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TotalDonate> TotalDonates { get; set; }
        public DbSet<Bonuse> Bonuses { get; set; }
        public DbSet<UserBonuse> UserBonuses { get; set; }
    }
}

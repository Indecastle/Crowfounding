using Crowfounding.Data;
using Crowfounding.Models;
using Crowfounding.Models.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services.Finances
{
    public class FinanceService : IFinanceService
    {
        private readonly ApplicationDbContext _db;

        public FinanceService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task DonateToCompany(User user, Company company, decimal amount)
        {
            user.Money -= amount;
            company.CurrentMoney += amount;
            await UpdateTotalDonate(user, company, amount);
            Donation donation = new Donation
            {
                CompanyId = company.Id,
                UserId = user.Id,
                MonyDonate = amount
            };
            Transaction transaction = new Transaction
            {
                CompanyId = company.Id,
                UserId = user.Id,
                Amount = amount,
                When = DateTime.Now,
                Type = TransactionType.User
            };
            await _db.Donations.AddAsync(donation);
            await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();
        }

        private async Task UpdateTotalDonate(User user, Company company, decimal amount)
        {
            var totaleDonate = _db.TotalDonates.SingleOrDefault(td => td.UserId == user.Id && td.CompanyId == company.Id);
            if (totaleDonate == null)
            {
                totaleDonate = new TotalDonate
                {
                    Amount = amount,
                    CompanyId = company.Id,
                    UserId = user.Id
                };
                _db.TotalDonates.AddAsync(totaleDonate);
            }
            else
            {
                totaleDonate.Amount += amount;
            }
        }
    }
}

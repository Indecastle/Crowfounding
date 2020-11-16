using Crowfounding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services.Finances
{
    public interface IFinanceService
    {
        Task DonateToCompany(User user, Company company, decimal Amount);
    }
}

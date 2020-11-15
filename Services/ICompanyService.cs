using Crowfounding.Models;
using Crowfounding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services
{
    public interface ICompanyService
    {
        Task CreateCompany(CompanyViewModel companyView);

        Task EditCompany(Company company, CompanyViewModel companyView);
    }
}

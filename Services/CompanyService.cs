using Crowfounding.Data;
using Crowfounding.Models;
using Crowfounding.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _db;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<User> _userManager;

        public CompanyService(ApplicationDbContext db, AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager)
        {
            _db = db;
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
        }

        public async Task CreateCompany(CompanyViewModel companyView)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var claimUser = authState.User;
            var owner = await _userManager.GetUserAsync(claimUser);
            Company company = new Company
            {
                Name = companyView.Name,
                Theme = companyView.Theme,
                End = companyView.End,
                NumberUsers = 0,
                Rating = 0,
                Description = companyView.Description,
                CurrentMoney = 0,
                NeedMoney = companyView.NeedMoney,
                URLVideo = companyView.URLVideo,
                DataCreate = DateTime.Now,
                LastEdit = DateTime.MinValue,
                EndAtNextDay = false,
                IsEnd = false,
                UserID = owner.Id,
                MainImage = companyView.MainImage,
                Bonuses = companyView.Bonuses.Select(bv => bv.GetUpdatedBonuse()).ToList()
            };
            if (companyView.Images != null)
            {
                company.CompanyImages = companyView.Images.Select(imgUrl => new ImagesCompany { PhotoPath = imgUrl }).ToList();
            }
            await _db.Companies.AddAsync(company);
            await _db.SaveChangesAsync();
        }

        public async Task EditCompany(Company company, CompanyViewModel companyView)
        {
            company.Name = companyView.Name;
            company.NeedMoney = companyView.NeedMoney;
            company.Theme = companyView.Theme;
            company.URLVideo = companyView.URLVideo;
            company.Description = companyView.Description;
            company.End = companyView.End;
            company.MainImage = companyView.MainImage;
            company.CompanyImages = companyView.Images.Select(imgUrl => new ImagesCompany { PhotoPath = imgUrl }).ToList();
            company.IsEnd = company.End < DateTime.Now;

            companyView.Bonuses.ForEach(bv => bv.UpdateBonuse());

            await _db.SaveChangesAsync();
        }
    }
}

using Crowfounding.Models;
using Crowfounding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.BusinessLogic
{
    public static class CompanyBusinessLogic
    {
        //public static Company CompanyEditLogic(Company company, EditCompanyViewModel model)
        //{
        //    if ( company != null && model != null) {
        //        company.Name = model.Name;
        //        company.Theme = model.Theme;
        //        company.Description = model.Description;
        //        company.End = model.End;
        //        company.NeedMoney = model.NeedMoney;
        //        company.URLVideo = model.URLVideo;
        //        company.LastEdit = DateTime.Now;
        //        return company;
        //    }
        //    else
        //    {
        //        return null;
        //    }       
        //}

        public static Company UpdateRatingCompany(Company company, Rating rating)
        {
            if (company != null)
            {
                company.NumberUsers = company.NumberUsers + 1;
                company.AllRating = company.AllRating + rating.CompanyRating;
                company.Rating = (double)company.AllRating / company.NumberUsers;
            }
            return company;
        }

        public static string UploadImageToServer(IFormFile image, string folderPath, bool many)
        {
            try
            {
                string folder = "images", uniqueFileName = null;
                if (many)
                    folder = "secondimages";

                string[] splitpass = image.FileName.Split("\\");
                string filename = splitpass[splitpass.Length - 1];

                string uploadsFolder = Path.Combine(folderPath, folder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                image.CopyTo(new FileStream(filePath, FileMode.Create));
                return uniqueFileName;
            }
            catch
            {
                return null;
            }
        }

        //public static string ChargeStripe(Company company, DonateViewModel donateViewModel)
        //{
        //    var customers = new CustomerService();
        //    var charges = new ChargeService();

        //    var customer = customers.Create(new CustomerCreateOptions
        //    {
        //        Email = donateViewModel.Email,
        //        Source = donateViewModel.Token
        //    });

        //    try
        //    {
        //        var chage = charges.Create(new ChargeCreateOptions
        //        {
        //            Amount = Convert.ToInt32(donateViewModel.Total),
        //            Description = "Test Payment",
        //            Currency = "usd",
        //            Customer = customer.Id,
        //            ReceiptEmail = donateViewModel.Email,
        //            Metadata = new Dictionary<string, string>()
        //        {
        //            {"OrderId","111"},
        //            {"Postcode","LEE111"}
        //        }
        //        });
        //        if (chage.Status == "succeeded")
        //        {
        //            string BalanceTransactionId = chage.BalanceTransactionId;

        //            company.CurrentMoney += Convert.ToInt32(donateViewModel.Total);
        //            return "succeeded";
        //        }
        //        return "unsucceeded";
        //    }
        //    catch (StripeException e)
        //    {
        //        return e.Message;
        //    }
        //}

        public static bool EndCompanyIfTime(Company company, DateTime dateNow = default(DateTime))
        {
            if (dateNow == default(DateTime))
            {
                dateNow = DateTime.Now;
            }
            if (dateNow.Date >= company.End.Date)
            {
                company.IsEnd = true;
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

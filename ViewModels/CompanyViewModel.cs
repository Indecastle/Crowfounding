using BlazorInputFile;
using Crowfounding.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.ViewModels
{
    public class CompanyViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Theme")]
        public Theme Theme { get; set; }

        [Required]
        [Display(Name = "Data End")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Need Money")]
        [Range(10, 100000, ErrorMessage = "Accommodation invalid (10-100000).")]
        public decimal NeedMoney { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string URLVideo { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true",
        ErrorMessage = "need Main Image.")]
        public bool isLoadedMainImage { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true",
        ErrorMessage = "CompanyImages Loadding.")]
        public bool isLoadedCompanyImages { get; set; }

        public string MainImage { get; set; }
        public List<string> Images { get; set; }

        public List<BonuseViewModel> Bonuses { get; set; } = new List<BonuseViewModel>();

        public void UpdateData(Company editCompany)
        {
            Name = editCompany.Name;
            NeedMoney = editCompany.NeedMoney;
            Theme = editCompany.Theme;
            URLVideo = editCompany.URLVideo;
            Description = editCompany.Description;
            End = editCompany.End;
            MainImage = editCompany.MainImage;
            Images = editCompany.CompanyImages?.Select(ci => ci.PhotoPath).ToList();
            isLoadedMainImage = true;
            isLoadedCompanyImages = true;

            Bonuses = editCompany.Bonuses.Select(b => new BonuseViewModel(b)).ToList();
        }
    }

    public class BonuseViewModel
    {
        public Bonuse bonuse;

        public BonuseViewModel(Bonuse bonuse)
        {
            this.bonuse = bonuse;

            NeedAmount = bonuse.NeedAmount;
            Title = bonuse.Title;
            Description = bonuse.Description;
            Definition = bonuse.Definition;
        }

        public decimal NeedAmount { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Definition { get; set; }

        public bool IsExpanded { get; set; }

        public Bonuse GetUpdatedBonuse()
        {
            if (bonuse == null)
            {
                bonuse = new Bonuse();
            }
            UpdateBonuse();

            return bonuse;
        }

        public void UpdateBonuse()
        {
            bonuse.NeedAmount = NeedAmount;
            bonuse.Title = Title;
            bonuse.Description = Description;
            bonuse.Definition = Definition;

        }
    }
}

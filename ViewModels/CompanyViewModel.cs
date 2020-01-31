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
        public int NeedMoney { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string URLVideo { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true",
        ErrorMessage = "need Main Image.")]
        public bool isLoadedMainImage { get; set; }
        
        public string MainImage { get; set; }
        public List<string> Images { get; set; }
    }
}

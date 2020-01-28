using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public enum Theme
    {
        Electronics,
        Education,
        Service,
        ITcompany,
        Food
    }

    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Theme Theme { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public int AllRating { get; set; }
        public int NumberUsers { get; set; }
        public double Rating { get; set; }
        public int CurrentMoney { get; set; }
        [Required]
        public int NeedMoney { get; set; }
        public string URLVideo { get; set; }
        public DateTime LastEdit { get; set; }
        public DateTime DataCreate { get; set; }
        [Required]
        public string MainImage { get; set; }
        public bool EndAtNextDay { get;set;}
        public bool IsEnd { get; set; }

        public Company()
        {
            Comments = new HashSet<Comment>();
        }

        public virtual ICollection<ImagesCompany> CompanyImages { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string UserID { get; set; }
        public virtual User Owner { get; set; } 

    }
}

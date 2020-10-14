using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int CompanyRating { get; set; }
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime When { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}

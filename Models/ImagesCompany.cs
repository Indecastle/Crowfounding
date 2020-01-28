using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class ImagesCompany
    {
        public int Id { get; set; }
        [Required]
        public string PhotoPath { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Images { get; set; }
    }
}

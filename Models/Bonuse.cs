using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class Bonuse
    {
        [Key]
        public Guid Id { get; set; }

        public decimal NeedAmount { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Definition { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models.Finance
{
    public class TotalDonate
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string UserId { get; set; }

        public int CompanyId { get; set; }

        public virtual User User { get; set; }

        public virtual Company Company { get; set; }
        
        public virtual ICollection<UserBonuse> UserBonuses { get; set; }
    }
}

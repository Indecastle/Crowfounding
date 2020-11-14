using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models.Finance
{
    public class Donation
    {
        public int Id { get; set; }
        public decimal MonyDonate { get; set; }
        public string UserId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User Payer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime When { get; set; }

        public string UserId { get; set; }

        public int? CompanyId { get; set; }


        public virtual Company Company { get; set; }

        public virtual User User { get; set; }
    }
}

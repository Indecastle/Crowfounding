﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int MonyDonate { get; set; }
        public string UserId { get; set; }
        public int CompanyId { get; set; }

        public virtual User Payer { get; set; }
    }
}
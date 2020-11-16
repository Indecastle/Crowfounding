using System;
using System.ComponentModel.DataAnnotations;

namespace Crowfounding.Models
{
    public class UserBonuse
    {
        [Key]
        public Guid Id { get; set; }
        
        public DateTime CreateAt { get; set; }

        public string UserId { get; set; }
        
        public virtual User User { get; set; }
        
        public int CompanyId { get; set; }
        
        public virtual Company Company { get; set; }
        
        public Guid BonuseId { get; set; }
        
        public virtual Bonuse Bonuse { get; set; }
    }
}
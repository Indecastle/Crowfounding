using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public string UserID { get; set; }

        public int CompanyId { get; set; }
        public virtual Company CompanyComment { get; set; }
        
        public Comment()
        {
            When = DateTime.Now;
        }
    }
}

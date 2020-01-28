using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowfounding.Models
{
    public class WebBrowser
    {
        public int Id { get; set; }
        public string NameWebBrowser { get; set; }
        public string DeviceName { get; set; }

        public string UserID { get; set; }
        public virtual User Owner { get; set; }
    }
}

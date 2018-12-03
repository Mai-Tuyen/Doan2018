using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public bool IsUpdate { get; set; }
    }
}

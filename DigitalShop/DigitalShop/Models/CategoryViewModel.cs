using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        public bool IsUpdate { get; set; }
    }
}

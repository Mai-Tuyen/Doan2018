using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalShop.Entity
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }
        public List<Import> Imports { get; set; }
    }
}

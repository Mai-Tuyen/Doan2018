using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalShop.Entity
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string DisplayName { get; set; }
        public string Phone { get; set; }
        [Column(TypeName = "ntext")]
        public string Address { get; set; }
        public bool Status { get; set; }
        public List<Order> Order { get; set; }
    }
}

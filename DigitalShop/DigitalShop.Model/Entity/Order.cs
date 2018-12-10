using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalShop.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public DateTime CreateAt { get; set; }
        public string ShipName { get; set; }
        public string ShipMobile { get; set; }
        public string ShipAddress { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalShop.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double PriceIn { get; set; }
        public double PriceOut { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public DateTime CreateAt { get; set; }
        [ForeignKey("Admin")]
        public int CreateBy { get; set; }
        public int ViewCount { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Admin Admin { get; set; }
        public List<ImagesDetail> ImagesDetails { get; set; }
        public List<ImportDetail> ImportDetails { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}

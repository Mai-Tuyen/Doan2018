using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string AvatarImage { get; set; }
        [Required]
        public string Image1 { get; set; }
        [Required]
        public string Image2 { get; set; }
        [Required]
        public string Image3 { get; set; }
        [Required]
        public double PriceIn { get; set; }
        [Required]
        public double PriceOut { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public int CreateBy { get; set; }
        public string NameCreateBy { get; set; }
        
        public int ViewCount { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool Status { get; set; }
        public bool IsUpdate { get; set; }
        public int Sold { get; set; }
    }
}

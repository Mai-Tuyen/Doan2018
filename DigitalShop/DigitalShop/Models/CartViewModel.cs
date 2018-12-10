using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class CartViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductAvatar { get; set; }
        public int ProDuctQuantity { get; set; }
        public double ProductPriceOut { get; set; }
    }
}

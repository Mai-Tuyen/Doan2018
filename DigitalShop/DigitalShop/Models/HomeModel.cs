using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class HomeModel
    {
        public List<ProductViewModel> TopNewPhone { get; set; }
        public List<ProductViewModel> TopNewLaptop { get; set; }
        public List<ProductViewModel> TopNewTV { get; set; }
        public List <ProductViewModel> TopNewAccessories { get; set; }
    }
}

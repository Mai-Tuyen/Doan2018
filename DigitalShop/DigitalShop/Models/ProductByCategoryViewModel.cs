using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class ProductByCategoryViewModel
    {
        public List<ProductViewModel> ListProductViewModel { get; set; }
        public List<ManufacturerViewModel> ListManufacturer { get; set; }
    }
}

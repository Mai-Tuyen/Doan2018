using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace DigitalShop.Controllers
{
    public class SingleProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public SingleProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index(int id)
        {
            var productViewModel = productRepository.GetListProduct()
                .Where(x=>x.Id==id)
                .Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    AvatarImage = productRepository.GetListImage(x.Id)[0],
                    Image1 = productRepository.GetListImage(x.Id)[1],
                    Image2 = productRepository.GetListImage(x.Id)[2],
                    Image3 = productRepository.GetListImage(x.Id)[3],
                    PriceIn = x.PriceIn,
                    PriceOut = x.PriceOut,
                    Category = x.Category.Name,
                    ManufacturerId = x.ManufacturerId,
                    Manufacturer = x.Manufacturer.Name,
                    CreateAt = x.CreateAt,
                    CreateBy = x.CreateBy,
                    NameCreateBy = x.Admin.UserName,
                    ViewCount = x.ViewCount,
                    Quantity = x.Quantity,
                    Status = x.Status
                }).Single();
            ViewBag.SingleProductName = productViewModel.Name;
            return PartialView("_SingleProduct",productViewModel);
        }
    }
}
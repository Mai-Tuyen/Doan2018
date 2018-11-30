using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetNewProduct()
        //{

        //}
    }
}
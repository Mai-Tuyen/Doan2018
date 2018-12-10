using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
namespace DigitalShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IManufacturerRepository manufacturerRepository;
        public ProductController(IProductRepository productRepository,
            IManufacturerRepository manufacturerRepository)
        {
            this.productRepository = productRepository;
            this.manufacturerRepository = manufacturerRepository;
        }
        public IActionResult Index(int id)
        {
            var singleProduct = productRepository.GetListProduct()
                .Where(x => x.Id == id).FirstOrDefault();
            singleProduct.ViewCount += 1;
            productRepository.Save();
            var productViewModel = productRepository.GetListProduct()
                .Where(x => x.Id == id && x.Status == true)
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
            return View("_SingleProduct", productViewModel);
        }

        [HttpGet]
        public IActionResult GetListProductByCategory(string categoryName,string searchProduct,int? manufacturerId,double? minPrice,double? maxPrice)
        {
            var listProductViewModel = productRepository.GetListProduct()
                .Where(x => x.Category.Name == categoryName && x.Status==true)
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
                }).ToList();
            if (!string.IsNullOrEmpty(searchProduct))
            {
                listProductViewModel = listProductViewModel.Where(x => x.Name.Trim().ToLower().Contains(searchProduct.Trim().ToLower())).ToList();
            }
            if (minPrice!=null)
            {
                listProductViewModel = listProductViewModel.Where(x => x.PriceOut >= minPrice).ToList();
            }
            if (maxPrice != null)
            {
                listProductViewModel = listProductViewModel.Where(x => x.PriceOut <= maxPrice).ToList();
            }
            if (manufacturerId!=null)
            {
                    listProductViewModel = listProductViewModel.Where(x => x.ManufacturerId == manufacturerId).ToList();
              
            }
            var listManufacturer = manufacturerRepository.GetListManufacture()
                .Where(x=>x.Status==true)
                .Select(x => new ManufacturerViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            var productByCategoryModel = new ProductByCategoryViewModel()
            {
                ListProductViewModel = listProductViewModel,
                ListManufacturer = listManufacturer
            };
            ViewBag.CategoryName = categoryName;
            ViewBag.ManufacturerId = manufacturerId;
            ViewBag.SearchProduct = searchProduct;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            return View("_ListProductByCategory", productByCategoryModel);
        }
    }
}
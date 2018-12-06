using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;

namespace DigitalShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var topNewPhone = productRepository.GetListProduct()
                .Where(x => x.Category.Name == CategoryName.SMART_PHONE)
                .OrderByDescending(x => x.CreateAt).Take(3)
                .Select(x => new ProductViewModel() {
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


            var topNewLaptop = productRepository.GetListProduct()
                .Where(x => x.Category.Name == CategoryName.LAPTOP)
                .OrderByDescending(x => x.CreateAt).Take(3)
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

            var topNewTivi = productRepository.GetListProduct()
                .Where(x => x.Category.Name == CategoryName.TIVI)
                .OrderByDescending(x => x.CreateAt).Take(3)
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

            var topNewAccessories = productRepository.GetListProduct()
                .Where(x => x.Category.Name == CategoryName.AUDIO || x.Category.Name == CategoryName.SMART_WATCH || x.Category.Name == CategoryName.HEAD_PHONE)
                .OrderByDescending(x => x.CreateAt).Take(3)
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

            var homeModel = new HomeModel()
            {
                TopNewPhone = topNewPhone,
                TopNewLaptop = topNewLaptop,
                TopNewTV = topNewTivi,
                TopNewAccessories = topNewAccessories,
            };
            return View(homeModel);  
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

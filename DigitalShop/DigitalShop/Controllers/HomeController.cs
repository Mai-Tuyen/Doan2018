using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Http;

namespace DigitalShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IProductRepository productRepository,
             IOrderRepository orderRepository,
             IHttpContextAccessor _httpContextAccessor)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this._httpContextAccessor = _httpContextAccessor;
        }
        public IActionResult Index()
        {
            var topNewPhone = productRepository.GetListProduct()
                .Where(x => x.Category.Name == CategoryName.SMART_PHONE && x.Status == true)
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
                .Where(x => x.Category.Name == CategoryName.LAPTOP && x.Status == true)
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
                .Where(x => x.Category.Name == CategoryName.TIVI && x.Status == true)
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
                .Where(x => (x.Category.Name == CategoryName.AUDIO || x.Category.Name == CategoryName.SMART_WATCH || x.Category.Name == CategoryName.HEAD_PHONE) && x.Status == true)
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

            var listProductViewModel = productRepository.GetListProduct()
               .Where(x => x.Status == true)
               .Select(x => new ProductViewModel
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
               })
               .ToList();
            var listOrderDetail = orderRepository.GetAllOrderDetail();

            foreach (var productViewModel in listProductViewModel)
            {
                productViewModel.Sold = 0;
                foreach (var orderDetail in listOrderDetail)
                {
                    if (orderDetail.ProductId == productViewModel.Id)
                    {
                        productViewModel.Sold += orderDetail.Quantity;
                    }
                }
            }

            var listTopSeller = listProductViewModel.OrderByDescending(x => x.Sold)
                .Take(10)
                .ToList();

            List<string> listProductName = new List<string>();
            foreach (var item in listProductViewModel)
            {
                listProductName.Add(item.Name);
            }

            var homeModel = new HomeModel()
            {
                TopNewPhone = topNewPhone,
                TopNewLaptop = topNewLaptop,
                TopNewTV = topNewTivi,
                TopNewAccessories = topNewAccessories,
                TopSeller = listTopSeller,
                AllProductName = listProductName
            };
            ViewBag.UserNameLogin = _httpContextAccessor.HttpContext.Request.Cookies["userName"];
            return View(homeModel);  
        }

        public JsonResult GetListProductName()
        {
            var listProductViewModel = productRepository.GetListProduct()
               .Where(x => x.Status == true)
               .Select(x => new ProductViewModel
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
               })
               .ToList();

            List<string> listProductName = new List<string>();
            foreach (var item in listProductViewModel)
            {
                listProductName.Add(item.Name);
            }
            return new JsonResult(listProductName);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewBag.UserNameLogin = _httpContextAccessor.HttpContext.Request.Cookies["userName"];
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            ViewBag.UserNameLogin = _httpContextAccessor.HttpContext.Request.Cookies["userName"];
            return View();
        }

        public IActionResult Error()
        {
            ViewBag.UserNameLogin = _httpContextAccessor.HttpContext.Request.Cookies["userName"];
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

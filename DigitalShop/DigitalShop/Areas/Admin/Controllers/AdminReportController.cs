using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminReportController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        public AdminReportController(IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetTopView()
        {
            var listTopViewModel = productRepository.GetListProduct()
                .Where(x=>x.Status==true)
                .OrderByDescending(x => x.ViewCount)
                .Take(10)
                .Select(x=> new ProductViewModel
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
            return PartialView("_ListTopView", listTopViewModel);
        }

        [HttpPost]
        public IActionResult GetTopSeller(DateTime? startDate,DateTime? endDate)
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
            var listOrderDetail = orderRepository.GetAllOrderDetail();
            var _startDate = startDate != null ? (DateTime)startDate : DateTime.Now;
            var _endDate = endDate != null ? (DateTime)endDate : DateTime.Now;
            if (startDate != null)
            {
                listOrderDetail = listOrderDetail.Where(x => x.Order.CreateAt >= _startDate).ToList();
                ViewBag.StartDate = _startDate.ToShortDateString();
            }
            if (endDate != null)
            {
                _endDate = _endDate.AddSeconds(86399);
                listOrderDetail = listOrderDetail.Where(x => x.Order.CreateAt <= _endDate).ToList();
                ViewBag.EndDate = _endDate.ToShortDateString();
            }

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
                .Where(x => x.Sold > 0)
                .Take(10)
                .ToList();

            return PartialView("_ListTopSeller",listTopSeller);
            
        }

        [HttpPost]
        public IActionResult GetOrderSumary(DateTime? startDate, DateTime? endDate)
        {
            var listOrderSumary = orderRepository.GetListOrder()
                .Where(x=>x.Status == StatusOrder.APPROVED)
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Code = x.Code,
                    CustomerName = x.Customer.UserName,
                    CreateAt = x.CreateAt,
                    ShipName = x.ShipName,
                    ShipMobile = x.ShipMobile,
                    ShipAddress = x.ShipAddress,
                    Status = x.Status
                }).ToList();
            double totalPriceIn = 0;
            double totalPriceOut = 0;
            double profit = 0;
            var _startDate = startDate!=null? (DateTime)startDate: DateTime.Now ;
            var _endDate = endDate != null ? (DateTime)endDate : DateTime.Now;
            if (startDate!=null)
            {
                listOrderSumary = listOrderSumary.Where(x => x.CreateAt >= _startDate).ToList();
                ViewBag.StartDate = _startDate.ToShortDateString();
            }
            if (endDate != null)
            {
                _endDate = _endDate.AddSeconds(86399);
                listOrderSumary = listOrderSumary.Where(x => x.CreateAt <= _endDate).ToList();
                ViewBag.EndDate = _endDate.ToShortDateString();
            }

            var listOrderDetail = orderRepository.GetAllOrderDetail().ToList();
            foreach (var order in listOrderSumary)
            {
                var orderDetail = listOrderDetail.Where(x => x.OrderId == order.Id).ToList();
                if (orderDetail!=null)
                {
                    foreach (var item in orderDetail)
                    {
                        totalPriceIn += item.Product.PriceIn * item.Quantity;
                        totalPriceOut += item.Product.PriceOut * item.Quantity;
                    }
                }
            }
            profit = totalPriceOut - totalPriceIn;
            ViewBag.TotalPriceIn = totalPriceIn;
            ViewBag.TotalPriceOut = totalPriceOut;
            ViewBag.Profit = profit;
            ViewBag.TotalOrder = listOrderSumary.Count;

            return PartialView("_OrderSumary", listOrderSumary);
        }
    }
}
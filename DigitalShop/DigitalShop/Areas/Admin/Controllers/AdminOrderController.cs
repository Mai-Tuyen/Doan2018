﻿using System;
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
    public class AdminOrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        public AdminOrderController(IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetListOrder()
        {
            var orderViewModels = orderRepository.GetListOrder()
                .Where(x=>x.Status=="Pending")
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
            return PartialView("_ListOrder", orderViewModels);
        }

        public IActionResult GetOrderdetail(int id)
        {
            ViewBag.OrderCode = orderRepository.GetById(id).Code;
            ViewBag.ShipName = orderRepository.GetById(id).ShipName;
            ViewBag.ShipMobile = orderRepository.GetById(id).ShipMobile;
            ViewBag.ShipAddress = orderRepository.GetById(id).ShipAddress;

            var orderDetailViewModels = orderRepository.GetListOrderDetail(id)
                .Select(x => new OrderDetailViewModel()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    OrderCode = x.Order.Code,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Prices = x.Product.PriceOut * x.Quantity
                }).ToList();

            ViewBag.TotalPrice = 0;
            foreach (var item in orderDetailViewModels)
            {
                ViewBag.TotalPrice += item.Prices;
            }
            return PartialView("_OrderDetail", orderDetailViewModels);
        }

        public string Approve(int id)
        {
            var errorMessage = "";
            var order = orderRepository.GetById(id);
            order.Status = "Approved";
            orderRepository.Save();
            var orderDetails = orderRepository.GetListOrderDetail(id);
            foreach (var item in orderDetails)
            {
                var product = productRepository.GetById(item.ProductId);
                if (product.Quantity >= item.Quantity)
                {
                    product.Quantity -= item.Quantity;
                }
                else
                {
                    errorMessage = "The quantity of products is not enough !";
                    return errorMessage;
                }
                productRepository.Save();
            }
            return errorMessage;
        }
        
        public void Cancel(int id)
        {
            var order = orderRepository.GetById(id);
            order.Status = "Cancel";
            orderRepository.Save();
        }

        public IActionResult AddNewOrder()
        {
            var productViewModel = productRepository.GetListProduct()
              .Where(x => x.Status == true)
              .Select(x => new ProductViewModel
              {
                  Id = x.Id,
                  Name = x.Name,
              }).ToList();
            return PartialView("_AddNewOrder",productViewModel);
        }

    }
}
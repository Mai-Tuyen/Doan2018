using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository orderRepository;
        public CartController(IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IHttpContextAccessor _httpContextAccessor
            )
        {
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
            this._httpContextAccessor = _httpContextAccessor;
            this.orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult  GetListProductInCart(List<CartViewModel> cartList)
        {
            double totalPrice = 0;
            foreach (var item in cartList)
            {
                item.ProductID = productRepository.GetListProduct().Where(x => x.Name == item.ProductName).FirstOrDefault().Id;
                item.ProductPriceOut = productRepository.GetListProduct().Where(x => x.Name == item.ProductName).FirstOrDefault().PriceOut;
                item.ProductAvatar = productRepository.GetListImage(productRepository.GetListProduct()
                    .Where(x => x.Name == item.ProductName)
                    .FirstOrDefault().Id).FirstOrDefault();
                totalPrice += item.ProductPriceOut * item.ProDuctQuantity;
            }
            ViewBag.CountProduct = cartList.Count;
            ViewBag.TotalPrice = totalPrice;

            var customer = customerRepository.GetListCustomer()
                .Where(x => x.UserName == _httpContextAccessor.HttpContext.Request.Cookies["emailCustomer"]).FirstOrDefault();
            ViewBag.ShipName = "";
            ViewBag.ShipMobile = "";
            ViewBag.ShipAddress = "";
            if (customer != null)
            {
                ViewBag.ShipName = customer.DisplayName;
                ViewBag.ShipMobile = customer.Phone;
                ViewBag.ShipAddress = customer.Address;
            }
            return PartialView("_CheckOut", cartList);
        }

        [HttpPost]
        public string CheckOut(OrderViewModel orderViewModel,List<OrderDetailViewModel> orderDetailViewModels)
        {
            var errorMessage = "";
            Order newOrder = new Order();

            var customer = customerRepository.GetListCustomer()
                .Where(x => x.UserName == _httpContextAccessor.HttpContext.Request.Cookies["emailCustomer"]).FirstOrDefault();
            
            if (customer!=null)
            {
                newOrder.CustomerId = customer.Id;
            }
            else
            {
                Customer newCustomer = new Customer();
                newCustomer.UserName = "NoName_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                newCustomer.Status = false;
                customerRepository.Add(newCustomer);
                newOrder.CustomerId = newCustomer.Id;
            }

            newOrder.Code = "OD" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            newOrder.CreateAt = DateTime.Now;
            newOrder.ShipName = orderViewModel.ShipName;
            newOrder.ShipMobile = orderViewModel.ShipMobile;
            newOrder.ShipAddress = orderViewModel.ShipAddress;
            newOrder.Status = StatusOrder.PENDING;
            orderRepository.Add(newOrder);
            foreach (var item in orderDetailViewModels)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = newOrder.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                orderRepository.AddOrderDetail(orderDetail);
            }
            return errorMessage;
        }
    }
}
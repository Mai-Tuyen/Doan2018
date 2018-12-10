using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;
        public CartController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult  GetListProductInCart(List<CartViewModel> cartList)
        {
            foreach (var item in cartList)
            {
                item.ProductID = productRepository.GetListProduct().Where(x => x.Name == item.ProductName).FirstOrDefault().Id;
                item.ProductPriceOut = productRepository.GetListProduct().Where(x => x.Name == item.ProductName).FirstOrDefault().PriceOut;
                item.ProductAvatar = productRepository.GetListImage(productRepository.GetListProduct()
                    .Where(x => x.Name == item.ProductName)
                    .FirstOrDefault().Id).FirstOrDefault();
            }
            return RedirectToAction("CheckOut", new { @cartList1 = cartList });
           
        }

        public IActionResult CheckOut( List<CartViewModel> cartList1)
        {
            //List<CartViewModel> cartList = TempData["cart"] as List<CartViewModel>;
            return View("_CheckOut",cartList1);
        }
    }
}
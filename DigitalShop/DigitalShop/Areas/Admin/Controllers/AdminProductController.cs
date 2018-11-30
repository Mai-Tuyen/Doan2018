using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public AdminProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetListProduct()
        {
            List<ProductViewModel> listProduct = new List<ProductViewModel>();
            var listProductEntity = productRepository.GetListProduct()
                .OrderBy(z => z.Status)
                .ThenBy(y => y.CreateAt);

                listProduct = listProductEntity.Select(x => new ProductViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     AvatarImage = productRepository.GetListImage(x.Id)[0],
                     Image1 = productRepository.GetListImage(x.Id)[0],
                     Image2 = productRepository.GetListImage(x.Id)[1],
                     Image3 = productRepository.GetListImage(x.Id)[2],
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
            return PartialView("_ListProduct", listProduct);
        }

        public void Deactive(int id)
        {
            var product = productRepository.GetById(id);
            product.Status = false;
            productRepository.Save();
        }

        public IActionResult EditAction(int? id)
        {
            var productViewModel = new ProductViewModel();
            if (id != null)
            {
                ViewBag.modalTitle = "Product";
                productViewModel = productRepository.GetListProduct()
                                    .Where(x => x.Id == id)
                                    .Select(x => new ProductViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Description = x.Description,
                                        AvatarImage = productRepository.GetListImage(x.Id)[0],
                                        Image1 = productRepository.GetListImage(x.Id)[0],
                                        Image2 = productRepository.GetListImage(x.Id)[1],
                                        Image3 = productRepository.GetListImage(x.Id)[2],
                                        PriceIn = x.PriceIn,
                                        PriceOut = x.PriceOut,
                                        CategoryId = x.CategoryId,
                                        Category = x.Category.Name,
                                        ManufacturerId = x.ManufacturerId,
                                        Manufacturer = x.Manufacturer.Name,
                                        CreateAt = x.CreateAt,
                                        CreateBy = x.CreateBy,
                                        NameCreateBy = x.Admin.UserName,
                                        ViewCount = x.ViewCount,
                                        Quantity = x.Quantity,
                                        Status = x.Status,
                                        IsUpdate = true
                                    }).Single();
            }
            else
            {
                ViewBag.modalTitle = "Product";
                productViewModel.IsUpdate = false;
                productViewModel.Status = true;
            }


            return PartialView("_UpdateProduct", productViewModel);
        }

        [HttpPost]
        public void Update(ProductViewModel productViewModel)
        {
            if (productViewModel.IsUpdate)
            {
                var product = productRepository.GetById(productViewModel.Id);
                product.Name = productViewModel.Name;
                product.Description = productViewModel.Description;
                product.Image = productViewModel.AvatarImage + "|" + productViewModel.Image1 + "|" + productViewModel.Image2 + "|" + productViewModel.Image3;
                product.PriceIn = productViewModel.PriceIn;
                product.PriceOut = productViewModel.PriceOut;
                product.CategoryId = productViewModel.CategoryId;
                product.ManufacturerId = productViewModel.ManufacturerId;
                product.CreateAt = productViewModel.CreateAt;
                product.CreateBy = productViewModel.CreateBy;
                product.Quantity = productViewModel.Quantity;
                product.Status = productViewModel.Status;
            }
            else
            {
                Product newproduct = new Product()
                {
                    Name = productViewModel.Name,
                    Description = productViewModel.Description,
                    Image = productViewModel.AvatarImage + "|" + productViewModel.Image1 + "|" + productViewModel.Image2 + "|" + productViewModel.Image3,
                    PriceIn = productViewModel.PriceIn,
                    PriceOut = productViewModel.PriceOut,
                    CategoryId = productViewModel.CategoryId,
                    ManufacturerId = productViewModel.ManufacturerId,
                    CreateAt = productViewModel.CreateAt,
                    CreateBy = productViewModel.CreateBy,
                    ViewCount = 0,
                    Quantity = productViewModel.Quantity,
                    Status = productViewModel.Status
                };
                productRepository.Add(newproduct);
            }
        }
    }
}
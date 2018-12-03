using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IManufacturerRepository manufacturerRepository;
        public AdminProductController(IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IManufacturerRepository manufacturerRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.manufacturerRepository = manufacturerRepository;
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
                                        Image1 = productRepository.GetListImage(x.Id)[1],
                                        Image2 = productRepository.GetListImage(x.Id)[2],
                                        Image3 = productRepository.GetListImage(x.Id)[3],
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
                productViewModel.CreateAt = DateTime.Now;
                productViewModel.NameCreateBy = User.Identity.Name;
                productViewModel.ViewCount = 0;
                productViewModel.Quantity = 0;
                productViewModel.Status = true;
            }
            ViewBag.CategoryId = new SelectList(categoryRepository.GetListCategory().OrderBy(x=>x.Name), "Id", "Name", productViewModel.CategoryId);
            ViewBag.ManufacturerId = new SelectList(manufacturerRepository.GetListManufacture().OrderBy(x => x.Name), "Id", "Name", productViewModel.ManufacturerId);
            return PartialView("_UpdateProduct", productViewModel);
        }

        [HttpPost]
        public string Update(ProductViewModel productViewModel, IFormFile chooseAvatar, IFormFile chooseImage1, IFormFile chooseImage2, IFormFile chooseImage3)
        {
            string errorMessage = "";
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
                product.ViewCount = productViewModel.ViewCount;
                product.Status = productViewModel.Status;
            }
            else
            {
                if (productRepository.GetListProduct().Any(x => x.Name.Trim().ToLower() == productViewModel.Name.Trim().ToLower()))
                {
                    errorMessage = "This product already exists !";
                    return errorMessage;
                }
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
            return errorMessage;
        }
    }
}
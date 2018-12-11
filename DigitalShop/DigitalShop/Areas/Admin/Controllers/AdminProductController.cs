using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IManufacturerRepository manufacturerRepository;
        private readonly IAdminRepository adminRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public AdminProductController(IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IManufacturerRepository manufacturerRepository,
            IHostingEnvironment hostingEnvironment,
            IAdminRepository adminRepository
            )
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.adminRepository = adminRepository;
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
        public string Update(ProductViewModel productViewModel)
        {
            var fileAvatar = Request.Form.Files.Where(x=>x.Name=="fileAvatar").FirstOrDefault();
            var fileImage1 = Request.Form.Files.Where(x => x.Name == "fileImage1").FirstOrDefault();
            var fileImage2 = Request.Form.Files.Where(x=>x.Name== "fileImage2").FirstOrDefault();
            var fileImage3 = Request.Form.Files.Where(x => x.Name == "fileImage3").FirstOrDefault();

            if (fileAvatar!=null)
            {
                var _fileAvatarName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_0." + fileAvatar.FileName.Split(".").LastOrDefault();
                var pathAvatar = Path.Combine(hostingEnvironment.WebRootPath + "\\images", _fileAvatarName);
                fileAvatar.CopyTo(new FileStream(pathAvatar, FileMode.Create));
                productViewModel.AvatarImage = "/images/"+ _fileAvatarName;
            }
            if (fileImage1 != null)
            {
                var _fileImage1 = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_1." + fileImage1.FileName.Split(".").LastOrDefault();
                var pathImage1 = Path.Combine(hostingEnvironment.WebRootPath + "\\images", _fileImage1);
                fileImage1.CopyTo(new FileStream(pathImage1, FileMode.Create));
                productViewModel.Image1 = "/images/" + _fileImage1;
            }
            if (fileImage2 != null)
            {
                var _fileImage2 = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_2." + fileImage2.FileName.Split(".").LastOrDefault();
                var pathImage2 = Path.Combine(hostingEnvironment.WebRootPath + "\\images", _fileImage2);
                fileImage2.CopyTo(new FileStream(pathImage2, FileMode.Create));
                productViewModel.Image2 = "/images/" + _fileImage2;
            }
            if (fileImage3 != null)
            {
                var _fileImage3 = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_3." + fileImage3.FileName.Split(".").LastOrDefault();
                var pathImage3 = Path.Combine(hostingEnvironment.WebRootPath + "\\images", _fileImage3);
                fileImage3.CopyTo(new FileStream(pathImage3, FileMode.Create));
                productViewModel.Image3 = "/images/" + _fileImage3;
            }

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
                product.CreateBy = adminRepository.GetByUserName(productViewModel.NameCreateBy).Id;
                product.Quantity = productViewModel.Quantity;
                product.ViewCount = productViewModel.ViewCount;
                product.Status = productViewModel.Status;
                productRepository.Save();
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
                    CreateBy = adminRepository.GetByUserName(productViewModel.NameCreateBy).Id,
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
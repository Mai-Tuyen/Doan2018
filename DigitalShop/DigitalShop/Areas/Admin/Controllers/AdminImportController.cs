using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminImportController : Controller
    {
        private readonly IImportRepository importRepository;
        private readonly IProductRepository productRepository;
        private readonly IAdminRepository adminRepository;
        public AdminImportController(IImportRepository importRepository,
            IProductRepository productRepository,
            IAdminRepository adminRepository)
        {
            this.importRepository = importRepository;
            this.productRepository = productRepository;
            this.adminRepository = adminRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetListImport()
        {
            var importViewModels = importRepository.GetListImport()
                .Select(x => new ImportViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    CreateAt = x.CreateAt,
                    NameCreateBy = x.Admin.UserName,
                    Detail = x.Detail
                }).ToList();
            return PartialView("_ListImport", importViewModels);
        }

        public IActionResult GetImportDetail(int id)
        {
            ViewBag.ImportCode = importRepository.GetById(id).Code;
            var importDetailViewModel = importRepository.GetListImportDetail(id)
                .Select(x => new ImportDetailViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Prices = x.Product.PriceIn * x.Quantity
                }).ToList();
            ViewBag.TotalPrice = 0;
            foreach (var item in importDetailViewModel)
            {
                ViewBag.TotalPrice += item.Prices;
            }
            return PartialView("_ImportDetail", importDetailViewModel);
        }

        [HttpGet]
        public IActionResult AddImport()
        {
            var productViewModel = productRepository.GetListProduct()
                .Where(x=>x.Status==true)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            return PartialView("_AddImport", productViewModel);
        }

        [HttpPost] 
        public string Update(ImportViewModel importViewModel,List<ImportDetailViewModel> listImportDetail)
        {
            string errorMessage = "";
            if (importRepository.GetListImport().Any(x => x.Code.Trim().ToLower() == importViewModel.Code.Trim().ToLower()))
            {
                errorMessage = "This product already exists !";
                return errorMessage;
            }
            Import newImport = new Import()
            {
                Code = importViewModel.Code,
                CreateBy = adminRepository.GetByUserName(User.Identity.Name).Id,
                CreateAt = DateTime.Now,
                Detail = importViewModel.Detail
            };
            importRepository.Add(newImport);

            foreach (var item in listImportDetail)
            {
                ImportDetail newImportDetail = new ImportDetail()
                {
                    ImportId= newImport.Id,
                    ProductId = item.ProductId,
                    Quantity =item.Quantity
                };
                importRepository.AddImportDetail(newImportDetail);

                var productChange = productRepository.GetById(newImportDetail.ProductId);
                productChange.Quantity += newImportDetail.Quantity;
            }
            return errorMessage;
        }
    }
}
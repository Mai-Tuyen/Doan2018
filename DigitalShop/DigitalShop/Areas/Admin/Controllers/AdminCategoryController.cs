using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public AdminCategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetListCategory()
        {
            List<CategoryViewModel> listCategoryViewModel = new List<CategoryViewModel>();
            var listCategoryEntity = categoryRepository.GetListCategory();
            listCategoryViewModel = listCategoryEntity.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();

            return PartialView("_ListCategory", listCategoryViewModel);
        }
    }
}

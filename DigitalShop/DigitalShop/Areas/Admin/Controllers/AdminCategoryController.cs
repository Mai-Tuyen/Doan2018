using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            listCategoryViewModel = listCategoryEntity.OrderBy(x=>x.Status).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();

            return PartialView("_ListCategory", listCategoryViewModel);
        }

        public void Deactive(int id)
        {
            var category = categoryRepository.GetById(id);
            category.Status = false;
            categoryRepository.Deactivate(id);
        }

        public IActionResult EditAction(int? id)
        {
            var categoryViewModel = new CategoryViewModel();
            if (id != null)
            {
                ViewBag.modalTitle = "Update Category";
                categoryViewModel = categoryRepository.GetListCategory()
                                    .Where(x => x.Id == id)
                                    .Select(s => new CategoryViewModel
                                    {
                                        Id = s.Id,
                                        Name = s.Name,
                                        Status = s.Status,
                                        IsUpdate = true,
                                    }).Single();
            }
            else
            {
                ViewBag.modalTitle = "Add Category";
                categoryViewModel.IsUpdate = false;
                categoryViewModel.Status = true;
            }
                
            
            return PartialView("_UpdateCategory", categoryViewModel);
        }

        [HttpPost]
        public string Update(CategoryViewModel categoryViewModel)
        {
            string errorMessage = "";
            if (categoryViewModel.IsUpdate)
            {
                var category = categoryRepository.GetById(categoryViewModel.Id);
                category.Name = categoryViewModel.Name;
                category.Status = categoryViewModel.Status;
                categoryRepository.Save();
            }
            else
            {
                if (categoryRepository.GetListCategory().Any(x => x.Name.Trim().ToLower() == categoryViewModel.Name.Trim().ToLower()))
                {
                    errorMessage = "This category already exists !";
                    return errorMessage;
                }
                Category category = new Category()
                {
                    Name = categoryViewModel.Name,
                    Status = categoryViewModel.Status
                };
                categoryRepository.Add(category);
            }
            return errorMessage;
        }
    }
}

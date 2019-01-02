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
    public class AdminManagementController : Controller
    {
        private readonly IAdminRepository adminRepository;
        public AdminManagementController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetListAdmin()
        {
            var rootAdmin = adminRepository.GetByUserName(User.Identity.Name);
            if (rootAdmin.Type != TypeAdmin.ROOT)
            {
                return PartialView("_NotHaveAccess");
            }
            var listAdmin = adminRepository.GetListAdmin();
            List<AdminViewModel> adminViewModels = new List<AdminViewModel>();
            adminViewModels = listAdmin.Select(x => new AdminViewModel
            {
                Id = x.Id,
                UserName =x.UserName,
                PassWord = x.PassWord,
                Type = x.Type,
                Status = x.Status
            }).ToList();
            return PartialView("_ListAdmin", adminViewModels);
        }

        public void Deactive(int id)
        {
            var admin = adminRepository.GetById(id);
            admin.Status = false;
            adminRepository.Deactivate(id);
        }

        public IActionResult EditAction(int? id)
        {
            var adminViewModel = new AdminViewModel();
            if (id != null)
            {
                ViewBag.modalTitle = "Update admin infomation";
                adminViewModel = adminRepository.GetListAdmin()
                                    .Where(x => x.Id == id)
                                    .Select(s => new AdminViewModel
                                    {
                                        Id = s.Id,
                                        UserName = s.UserName,
                                        PassWord = s.PassWord,
                                        Type = s.Type,
                                        Status = s.Status,
                                        IsUpdate = true,
                                    }).Single();
            }
            else
            {
                ViewBag.modalTitle = "Add new admin";
                adminViewModel.IsUpdate = false;
                adminViewModel.Status = true;
            }
            return PartialView("_UpdateAdmin", adminViewModel);
        }

        [HttpPost]
        public string Update(AdminViewModel adminViewModel)
        {
            string errorMessage="";
            if (adminViewModel.IsUpdate)
            {
                var admin = adminRepository.GetById(adminViewModel.Id);
                admin.UserName = adminViewModel.UserName;
                admin.PassWord = adminViewModel.PassWord;
                admin.Type = adminViewModel.Type;
                admin.Status = adminViewModel.Status;
                adminRepository.Save();
            }
            else
            {
                if (adminRepository.GetListAdmin().Any(x=>x.UserName.Trim().ToLower()==adminViewModel.UserName.Trim().ToLower()))
                {
                     errorMessage = "This user name already exists !";
                    return errorMessage;
                }
                DigitalShop.Entity.Admin admin = new DigitalShop.Entity.Admin()
                {
                    UserName = adminViewModel.UserName,
                    PassWord = adminViewModel.PassWord,
                    Type = adminViewModel.Type,
                    Status = adminViewModel.Status,
                };
                adminRepository.Add(admin);
            }
            return errorMessage;
        }

        [HttpPost]
        public bool ChangePassWord(string userName,string newPassWord,string confirmNewPassWord)
        {
            var admin = adminRepository.GetListAdmin()
                .Where(x => x.UserName == userName).FirstOrDefault();
            if (admin != null && newPassWord.Trim()== confirmNewPassWord.Trim())
            {
                admin.PassWord = newPassWord;
                adminRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
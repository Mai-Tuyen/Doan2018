using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Service.IRepository;
using DigitalShop.Models;
using Microsoft.AspNetCore.Mvc;
using DigitalShop.Entity;
using Microsoft.AspNetCore.Authorization;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminManufacturerController : Controller
    {
        private readonly IManufacturerRepository manufacturerRepository;
        public AdminManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetListManufacturer()
        {
            List<ManufacturerViewModel> listManufacturerViewModel = new List<ManufacturerViewModel>();
            var listManufacturerEntity = manufacturerRepository.GetListManufacture();
            listManufacturerViewModel = listManufacturerEntity.OrderBy(x=>x.Status).Select(x => new ManufacturerViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            }).ToList();

            return PartialView("_ListManufacturer", listManufacturerViewModel);
        }

        public void Deactive(int id)
        {
            var manufacturer = manufacturerRepository.GetById(id);
            manufacturer.Status = false;
            manufacturerRepository.Deactive(id);
        }

        public IActionResult EditAction(int? id)
        {
            var ManufacturerViewModel = new ManufacturerViewModel();
            if (id != null)
            {
                ViewBag.modalTitle = "Update Manufacturer";
                ManufacturerViewModel = manufacturerRepository.GetListManufacture()
                                    .Where(x => x.Id == id)
                                    .Select(s => new ManufacturerViewModel
                                    {
                                        Id = s.Id,
                                        Name = s.Name,
                                        Status = s.Status,
                                        IsUpdate = true,
                                    }).Single();
            }
            else
            {
                ViewBag.modalTitle = "Add Manufacturer";
                ManufacturerViewModel.IsUpdate = false;
                ManufacturerViewModel.Status = true;
            }


            return PartialView("_UpdateManufacturer", ManufacturerViewModel);
        }

        [HttpPost]
        public string Update(ManufacturerViewModel manufacturerViewModel)
        {
            string errorMessage = "";
            if (manufacturerViewModel.IsUpdate)
            {
                var manufacturer = manufacturerRepository.GetById(manufacturerViewModel.Id);
                manufacturer.Name = manufacturerViewModel.Name;
                manufacturer.Status = manufacturerViewModel.Status;
                manufacturerRepository.Save();
            }
            else
            {
                if (manufacturerRepository.GetListManufacture().Any(x => x.Name.Trim().ToLower() == manufacturerViewModel.Name.Trim().ToLower()))
                {
                    errorMessage = "This manufacturer name already exists !";
                    return errorMessage;
                }
                Manufacturer manufacturer = new Manufacturer()
                {
                    Name = manufacturerViewModel.Name,
                    Status = manufacturerViewModel.Status
                };
                manufacturerRepository.Add(manufacturer);
            }
            return errorMessage;
        }
    }
}
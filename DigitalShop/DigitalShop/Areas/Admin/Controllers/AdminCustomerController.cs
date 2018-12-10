using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminCustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        public AdminCustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetListCustomer()
        {
            var listCustomer = customerRepository.GetListCustomer()
                .Select(x => new CustomerViewModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    DisplayName = x.DisplayName,
                    Phone = x.Phone,
                    Address = x.Address,
                    Status = x.Status
                }).ToList();
            return PartialView("_ListCustomer", listCustomer);
        }

        public void Deactivate(int id)
        {
            customerRepository.Deactivate(id);
        }
        public void Activate(int id)
        {
            customerRepository.Activate(id);
        }
    }
}
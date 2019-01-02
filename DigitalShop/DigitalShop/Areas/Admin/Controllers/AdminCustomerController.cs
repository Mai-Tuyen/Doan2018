using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminCustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AdminCustomerController(ICustomerRepository customerRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.customerRepository = customerRepository;
            this.httpContextAccessor = httpContextAccessor;
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
            var customerDeactivate = customerRepository.GetById(id);
            var userNamecustomerLogin = httpContextAccessor.HttpContext.Request.Cookies["emailCustomer"];
            if (customerDeactivate.UserName== userNamecustomerLogin)
            {
                Response.Cookies.Delete("userName");
                Response.Cookies.Delete("emailCustomer");
            }
        }
        public void Activate(int id)
        {
            customerRepository.Activate(id);
        }
    }
}
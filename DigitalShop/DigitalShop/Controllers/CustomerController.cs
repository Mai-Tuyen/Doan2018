using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        [HttpPost]
        public string Register(Customer newCustomer)
        {
            var errorMessage = "";
            foreach (var item in customerRepository.GetListCustomer())
            {
                if (item.UserName.Trim().ToLower() == newCustomer.UserName.Trim().ToLower())
                {
                    errorMessage = "This user name  already exists ! Please try again !";
                    return errorMessage;
                }
            }
            newCustomer.Status = true;
            customerRepository.Add(newCustomer);
            return errorMessage;
        }

        [HttpPost]
        public IActionResult Login(string userName, string passWord)
        {
            foreach (var item in customerRepository.GetListCustomer())
            {

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
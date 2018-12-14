using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalShop.Entity;
using DigitalShop.Models;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerController(ICustomerRepository customerRepository,
            IHttpContextAccessor _httpContextAccessor)
        {
            this.customerRepository = customerRepository;
            this._httpContextAccessor = _httpContextAccessor;
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
        public string Login(string userName, string passWord,bool rememberme)
        {
            var errorMessage = "";
            var customer = customerRepository.GetListCustomer()
                .Where(x => x.UserName.Trim().ToLower() == userName.Trim().ToLower() && x.PassWord.Trim().ToLower() == passWord.Trim().ToLower()).FirstOrDefault();
           
            if (customer==null)
            {
                errorMessage = "Incorrect username or password !";
            }
            else
            {
                if (!customer.Status)
                {
                    errorMessage = "Your account has been locked !";
                    return errorMessage;
                }
                else
                {
                    CookieOptions option = new CookieOptions();
                    if (rememberme)
                    {
                        option.Expires = DateTime.Now.AddDays(10);
                    }
                    else
                    {
                        option.Expires = DateTime.Now.AddHours(1);
                    }

                    Response.Cookies.Append("userName", customer.DisplayName, option);
                    Response.Cookies.Append("emailCustomer", customer.UserName, option);
                    ViewBag.UserName = userName;
                }
                
            }
            return errorMessage;
        }

        public void Logout()
        {
            Response.Cookies.Delete("userName");
            Response.Cookies.Delete("emailCustomer");
        }

        public IActionResult GetCustomerInfomation()
        {
            var userName = _httpContextAccessor.HttpContext.Request.Cookies["emailCustomer"];
            var customer = customerRepository.GetListCustomer()
                .Where(x => x.UserName == userName).FirstOrDefault();
            ViewBag.modalTitle = "Customer Infomation";
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Id = customer.Id,
                UserName = customer.UserName,
                PassWord = customer.PassWord,
                DisplayName = customer.DisplayName,
                Phone = customer.Phone,
                Address = customer.Address,
                Status = customer.Status
            };
            return PartialView("_CustomerInfomation",customerViewModel);
        }

        [HttpPost]
        public string ChangeCustomerInfomation(CustomerViewModel customerViewModel)
        {
            string errorMessage="";
            var customer = customerRepository.GetById(customerViewModel.Id);
            customer.Address = customerViewModel.Address;
            customer.Phone = customerViewModel.Phone;
            if (!string.IsNullOrEmpty(customerViewModel.ConfirmPassWord))
            {
                if (customerViewModel.PassWord.Trim().ToLower() == customerViewModel.ConfirmPassWord.Trim().ToLower())
                {
                    customer.PassWord = customerViewModel.PassWord;
                }
                else
                {
                    errorMessage = "Not match new Password !";
                }
            }
            customerRepository.Save();
            return errorMessage;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DigitalShop.Service.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DigitalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly IAdminRepository adminRepository;

        public AdminLoginController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public IActionResult Index()
        {
            return View("_AdminLogin");
        }

        [HttpGet]
        public async Task<bool> CheckLogin(string userName, string passWord, bool rememberme)
        {
            var check = adminRepository.CheckAdminLogin(userName, passWord);
            if (check)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, passWord));
                var principal = new ClaimsPrincipal(identity);
                if (rememberme)
                {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false, AllowRefresh =false, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)});
                }
                else
                {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false, AllowRefresh = false, ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), RedirectUri = "/Admin/AdminLogin/Index" });
                }

                return true;
            }
            else return false;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
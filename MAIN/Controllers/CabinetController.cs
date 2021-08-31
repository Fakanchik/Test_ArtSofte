using BuissnesLayer;
using DataLayer.Entityes;
using MAIN.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAIN.Controllers
{
    public class CabinetController : Controller
    {
        private AccountManager _accountManager;
        public CabinetController(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        public IActionResult Index(UserModel userModel)
        {
            if (User.Identity.IsAuthenticated) 
            {
                User userData = _accountManager.Users.GetUserByPhone(User.Identity.Name);
                return View(userData);
            } 
            else return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

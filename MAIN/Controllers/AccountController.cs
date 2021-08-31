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
using System.Security.Claims;
using System.Threading.Tasks;
using static MAIN.Models.UserModel;


namespace MAIN.Controllers
{
    public class AccountController : Controller
    {
        private AccountManager _accountManager;
        public AccountController(AccountManager accountManager) 
        {
            _accountManager = accountManager;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Cabinet");
            else return View();
        }
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Cabinet");
            else return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                User _newUser = new User(){Phone = userModel.userCreate.Phone, Email = userModel.userCreate.Email, FIO = userModel.userCreate.FIO, Password = userModel.userCreate.Password };
                if (!_accountManager.Users.RegisteredUserForRegister(userModel.userCreate.Phone, userModel.userCreate.Email))
                {
                    _accountManager.Users.CreateUser(_newUser);
                    TempData["FIO"] = userModel.userCreate.FIO;
                    return RedirectToAction("Complete");
                }
                ModelState.AddModelError("userCreate.Phone", "Данный номер телефона или почта уже зарегистрирована!");    
            }
            return View(userModel);
        }
        public IActionResult Complete()
        {
                ViewBag.Message = "Поздравляем, " + TempData["FIO"] + ", Вы стали новым пользователем системы!";
                return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Cabinet");
            else return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_accountManager.Users.RegisteredUser(userModel.userAuth.Phone))
                {
                   if (_accountManager.Users.CorrectPassword(userModel.userAuth.Phone, userModel.userAuth.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType,userModel.userAuth.Phone)
                        };
                        ClaimsIdentity phone = new ClaimsIdentity(claims,"ApplicationCookie",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(phone));
                        _accountManager.Users.LoginDate(userModel.userAuth.Phone);
                        return RedirectToAction("Index", "Cabinet");
                    }
                   else  ModelState.AddModelError("userAuth.Password", "Введен некорректный пароль!");
                }
                else ModelState.AddModelError("userAuth.Phone", "По данному номеру телефона аккаунт не найден.");
            } 
            return View(userModel);
        }
    }
}

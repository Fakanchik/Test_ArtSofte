using BuissnesLayer;
using BuissnesLayer.Interfaces;
using DataLayer;
using DataLayer.Entityes;
using MAIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MAIN.Controllers
{
    public class HomeController : Controller
    {

        private AccountManager _accountManager;
        public HomeController(AccountManager accountManager)
        {

            _accountManager = accountManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Cabinet");
            else return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

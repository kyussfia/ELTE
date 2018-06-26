using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zh.Models;
using Zh.Models.Db;

namespace Zh.Controllers
{
    public class HomeController : Controller
    {
        protected IErrorInterface  service;

        public HomeController(IErrorInterface ser)
        {
            service = ser;
        }

        public IActionResult Index()
        {
            ViewBag.Errors = service.GetLast30Errors().ToList();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

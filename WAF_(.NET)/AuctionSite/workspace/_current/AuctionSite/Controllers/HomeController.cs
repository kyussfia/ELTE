using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionSite.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(Models.IAuctionInterface service) 
        {
            auctionService = service;
        }

        [HttpGet]
        public IActionResult Index(bool? logout, bool? login, bool? registered)
        {
            feedMenu();

            if (logout.HasValue && logout.Equals(true))
            {
                ViewBag.LoggedMessage = "Successfully Logged out!";
            }
            
            if (login.HasValue && login.Equals(true))
            {
                ViewBag.LoggedMessage = "Successfully signed in!";
            }

            if (registered.HasValue && registered.Equals(true))
            {
                ViewBag.LoggedMessage = "Successful registration! Sign in!";
            }

            ViewBag.Items = auctionService.GetLast20ActiveItems().ToList();

            return View();
        }

        [HttpGet]
        public IActionResult AspIndex()
        {
            feedMenu();

            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            feedMenu();

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            feedMenu();

            return View();
        }
    }
}

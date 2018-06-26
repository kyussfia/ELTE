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

        public IActionResult Index()
        {
            feedMenu();

            ViewBag.Items = auctionService.GetLast20ActiveItems().ToList();

            return View();
        }

        public IActionResult AspIndex()
        {
            feedMenu();

            return View();
        }

        public IActionResult About()
        {
            feedMenu();

            return View();
        }

        public IActionResult Contact()
        {
            feedMenu();

            return View();
        }
    }
}

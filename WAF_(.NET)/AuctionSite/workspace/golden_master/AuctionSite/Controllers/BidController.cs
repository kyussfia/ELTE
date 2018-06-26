using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Controllers
{
    public class BidController : BaseController
    {
        public BidController(Models.IAuctionInterface service)
        {
            auctionService = service;
        }

        [HttpGet]
        public IActionResult Create(Int32 itemIdInt)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                feedMenu();
                ViewBag.Item = auctionService.GetItem(itemIdInt);

                return View();
            }
            return NotFound("Unauthorized error.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Int32 itemIdInt, Models.ViewModel.NewBidViewModel form)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                feedMenu();
                var item = auctionService.GetItem(itemIdInt);
                if (!ModelState.IsValid)
                {
                    ViewBag.Item = item;
                    ModelState.AddModelError("Price", "Your bid price must be greater than the current price.");
                    return View("Create", form);
                }

                if (!auctionService.createBid(item, GetCurrentUser(), form))
                {
                    ViewBag.Item = item;
                    ModelState.AddModelError("Price", "Your bid price must be greater than the current price.");
                    return View("Create", form);
                }

                return RedirectToAction("Get", "Item", new { itemId = itemIdInt });
            }
            return NotFound("Unauthorized error.");
        }
    }
}
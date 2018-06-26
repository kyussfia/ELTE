using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Controllers
{
    public class ItemController : BaseController
    {
        public ItemController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        [HttpGet]
        public ActionResult Get(Int32 itemId)
        {
            if (!auctionService.IsItemExist(itemId))
            {
                return NotFound();
            }
            var item = auctionService.GetItem(itemId);

            if (null == item)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }

            feedMenu();
            if (GetCurrentUser() != null)
            {
                ViewBag.CurrentUserId = GetCurrentUser().Id;
            }

            return View("Get", item);
        }

        [HttpGet]
        public IActionResult MyItems(string currentFilter, string searchString, int? page)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                Models.Entities.User user = GetCurrentUser();
                if (null == user)
                {
                    return NotFound("User not found");
                }

                if (searchString != null)
                {
                    page = 1;
                }

                ViewBag.CurrentFilter = searchString;
                ViewBag.CurrentUserId = user.Id;

                feedMenu();

                IQueryable<Models.Entities.Item> items;

                if (!String.IsNullOrEmpty(searchString))
                {
                    items = auctionService.GetItemsBiddedByUserFilteredByName(user, searchString);
                }
                else
                {
                    items = auctionService.GetItemsBiddedByUser(user);
                }

                int pageSize = 20;

                return View(PaginatedList<Models.Entities.Item>.Create(items.OrderByDescending(i => i.LastUpdate), page ?? 1, pageSize));
            }
            return NotFound("Unauthorized error.");
        }

        [HttpGet]
        public IActionResult Image(Int32 itemId)
        {
            if (!auctionService.IsItemExist(itemId))
            {
                //maybe throw exception
                return NotFound();
            }

            Byte[] img = auctionService.GetItemOnly(itemId).Picture;

            if (img == null) // nincs kép megadva
            {
                return File("~/images/NoImage.png", "image/png");
            }

            return File(img, "image/png");
        }
    }
}
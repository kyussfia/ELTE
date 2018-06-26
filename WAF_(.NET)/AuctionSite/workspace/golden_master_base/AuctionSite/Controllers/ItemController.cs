using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting.Server;

namespace AuctionSite.Controllers
{
    public class ItemController : BaseController
    {
        public ItemController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        public ActionResult Get(Int32? itemId)
        {
            if (!auctionService.IsItemExist(itemId))
            {
                return NotFound();
            }
            var item = auctionService.GetItem(itemId.Value);

            if (null == item)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }

            feedMenu();

            return View("Get", item);
        }

        public FileResult Image(Int32? itemId)
        {
            if (!auctionService.IsItemExist(itemId))
            {
                //maybe throw exception
                return File("~/App_Data/NoImage.png", "image/png");
            }

            Byte[] img = auctionService.GetItemOnly(itemId.Value).Picture;

            if (img == null) // nincs kép megadva
            {
                return File("~/App_Data/NoImage.png", "image/png");
            }

            return File(img, "image/png");
        }
    }
}
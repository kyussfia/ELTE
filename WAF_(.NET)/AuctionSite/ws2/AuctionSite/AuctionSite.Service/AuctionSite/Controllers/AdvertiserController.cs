using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionSite.Models;

namespace AuctionSite.Controllers
{
    public class AdvertiserController : BaseController
    {
        public AdvertiserController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        [HttpGet]
        public IActionResult List(Int32 advertiserId, string currentFilter, string searchString, int? page)
        {
            if (!auctionService.IsAdvertiserExist(advertiserId))
            {
                return NotFound();
            }

            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = searchString;

            feedMenu();
            ViewBag.CurrentAdvertiser = auctionService.GetAdvertiser(advertiserId);

            IQueryable<Models.Entities.Item> items;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = auctionService.GetValidItemsByAdvertiserFilteredByName(advertiserId, searchString);
            }
            else
            {
                items = auctionService.GetValidItemsByAdvertiser(advertiserId);
            }

            int pageSize = 20;

            return View(PaginatedList<Models.Entities.Item>.Create(items.OrderByDescending(i => i.CreatedAt), page ?? 1, pageSize));
        }
    }
}
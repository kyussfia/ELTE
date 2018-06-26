using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models;

namespace AuctionSite.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        [HttpGet]
        public IActionResult List(Int32 categoryId, string currentFilter, string searchString, int? page)
        {
            if (!auctionService.IsCategoryExist(categoryId))
            {
                return NotFound();
            }

            if (searchString != null)
            {
                page = 1;
            }

            ViewBag.CurrentFilter = searchString;

            feedMenu();
            ViewBag.CurrentCategory = auctionService.GetCategory(categoryId);

            IQueryable<Models.Entities.Item> items;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = auctionService.GetValidItemsByCategoryFilteredByName(categoryId, searchString);
            } else
            {
                items = auctionService.GetValidItemsByCategory(categoryId);
            }

            int pageSize = 20;

            return View(PaginatedList<Models.Entities.Item>.Create(items.OrderByDescending(i => i.CreatedAt), page ?? 1, pageSize));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuctionSite.Controllers
{
    public class BaseController : Controller
    {
        protected Models.IAuctionInterface auctionService;

        protected void feedMenu()
        {
            ViewBag.Categories = auctionService.GetCategories().ToArray();
        }
    }
}
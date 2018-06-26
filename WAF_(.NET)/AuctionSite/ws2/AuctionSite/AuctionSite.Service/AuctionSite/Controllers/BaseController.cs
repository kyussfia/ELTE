using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Controllers
{
    public class BaseController : Controller
    {
        protected Models.IAuctionInterface auctionService;

        protected void feedMenu()
        {
            ViewBag.CurrentAdv = HttpContext.Session.GetString("advertiser");
            ViewBag.CurrentUser = HttpContext.Session.GetString("user");
            ViewBag.Categories = auctionService.GetCategories().ToArray();
        }

        protected Models.Entities.User GetCurrentUser()
        {
            return auctionService.GetUserByUsername(HttpContext.Session.GetString("user"));
        }

        protected Models.Entities.User GetCurrentAdvertiser()
        {
            return auctionService.GetUserByUsername(HttpContext.Session.GetString("advertiser"));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using AuctionSite.Models;
using System.Diagnostics;

namespace AuctionSite.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        public IActionResult Error()
        {
            feedMenu();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
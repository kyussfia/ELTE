using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using Microsoft.AspNetCore.Routing;

namespace WebApplication.Controllers
{
    public class AnimalsController : Controller
    {

        protected ApplicationContext _context;

        public AnimalsController(ApplicationContext em)
        {
            _context = em;
        }

        [HttpGet]
        public ActionResult Get(Int32 errorId)
        {
            if (!_context.Animals.Any(a => a.Id == errorId))
            {
                return NotFound();
            }
            var item = _context.Animals.Single(a => a.Id == errorId);

            if (null == item)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }

            return View("Get", item);
        }

        [HttpGet]
        public IActionResult Image(Int32 errId)
        {
            if (!_context.Animals.Any(a => a.Id == errId))
            {
                //maybe throw exception
                return NotFound();
            }

            Byte[] img = _context.Animals.Single(a => a.Id == errId).Picture;

            if (img == null) // nincs kép megadva
            {
                return File("~/images/NoImage.png", "image/png");
            }

            return File(img, "image/png");
        }
    }
}
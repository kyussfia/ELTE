using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zh.Models;
using Zh.Models.Db;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Zh.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly IErrorInterface _context;

        public ErrorsController(IErrorInterface context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get(Int32 errorId)  
        {
            if (!_context.IsErrorExist(errorId))
            {
                return NotFound();
            }
            var item = _context.GetError(errorId);

            if (null == item)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }

            return View("Get", item);
        }

        [HttpGet]
        public IActionResult Image(Int32 errId)
        {
            if (!_context.IsErrorExist(errId))
            {
                //maybe throw exception
                return NotFound();
            }

            Byte[] img = _context.GetError(errId).Picture;

            if (img == null) // nincs kép megadva
            {
                return File("~/images/NoImage.png", "image/png");
            }

            return File(img, "image/png");
        }

        [HttpGet]
        public IActionResult RegisterError()
        {
            return View("RegisterError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createError(Int32 errorId, Models.ViewModel.NewErrorViewModel form)
        {
            var targetView = "RegisterError";
            // végrehajtjuk az ellenőrzéseket
            if (!ModelState.IsValid)
                return View(targetView, form);

            string[] validExts = { ".png", ".jpg", ".jpeg" };

            if (form.Picture != null && !validExts.Contains(Path.GetExtension(form.Picture.FileName)))
            {
                System.Diagnostics.Debug.WriteLine(form.Picture.FileName);
                ModelState.AddModelError("Picture", "Csak képformátum!");
                return View("RegisterError", form);
            }

            if (!_context.createError(form))
            {
                //ModelState.AddModelError("Picture", "Csak képformátum!");
                return View("RegisterError", form);
            }

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Get", "Errors", new { errId =  });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace AuctionSite.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        [HttpGet]
        public IActionResult RegisterAdvertiser()
        {
            feedMenu();
            return View("RegisterAdvertiser");
        }

        [HttpGet]
        public IActionResult RegisterCustomer()
        {
            feedMenu();
            return View("RegisterCustomer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdvertiser(Models.ViewModel.RegisterAdvertiserViewModel form)
        {
            feedMenu();
            var targetView = "RegisterAdvertiser";
            // végrehajtjuk az ellenőrzéseket
            if (!ModelState.IsValid)
                return View(targetView, form);

            if (!auctionService.RegisterAdvertiser(form))
            {
                ModelState.AddModelError("Username", "A megadott felhasználónév már létezik.");
                return View(targetView, form);
            }

            ViewBag.Information = "A regisztráció sikeres volt. Kérjük, jelentkezzen be.";

            if (HttpContext.Session.GetString("user") != null) // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
            {
                HttpContext.Session.Remove("user");
            }

            return RedirectToAction("Index", "Home", new { registered = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCustomer(Models.ViewModel.RegisterCustomerViewModel form)
        {
            feedMenu();
            var targetView = "RegisterCustomer";
            // végrehajtjuk az ellenőrzéseket
            if (!ModelState.IsValid)
                return View(targetView, form);

            if (!auctionService.RegisterCustomer(form))
            {
                ModelState.AddModelError("Username", "A megadott felhasználónév már létezik.");
                return View(targetView, form);
            }

            ViewBag.Information = "A regisztráció sikeres volt. Kérjük, jelentkezzen be.";

            if (HttpContext.Session.GetString("advertiser") != null) // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
            {
                HttpContext.Session.Remove("advertiser");
            }

            return RedirectToAction("Index", "Home", new { registered = true });
        }

        [HttpGet]
        public IActionResult LoginAdvertiser()
        {
            feedMenu();
            return View("LoginAdvertiser");
        }

        [HttpGet]
        public IActionResult LoginCustomer()
        {
            feedMenu();
            return View("LoginCustomer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginCustomer(Models.ViewModel.LoginViewModel user)
        {
            feedMenu();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hibás felhasználónév-jelszó páros.");
                user.IsValid = false;
                return View("LoginCustomer", user);
            }

            // bejelentkeztetjük a felhasználót
            if (!auctionService.LoginCustomer(user))
            {
                // nem szeretnénk, ha a felhasználó tudná, hogy a felhasználónévvel, vagy a jelszóval van-e baj, így csak általános hibát jelzünk
                ModelState.AddModelError("", "Hibás felhasználónév-jelszó páros.");
                user.IsValid = false;
                return View("LoginCustomer", user);
            }

            // ha sikeres volt az ellenőrzés
            HttpContext.Session.SetString("user", user.Username); // felvesszük a felhasználó nevét a munkamenetbe

            return RedirectToAction("Index", "Home", new { login = true }); // átirányítjuk a főoldalra
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAdvertiser(Models.ViewModel.LoginViewModel user)
        {
            feedMenu();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hibás felhasználónév-jelszó páros.");
                user.IsValid = false;
                return View("LoginAdvertiser", user);
            }

            // bejelentkeztetjük a felhasználót
            if (!auctionService.LoginAdvertiser(user))  
            {
                // nem szeretnénk, ha a felhasználó tudná, hogy a felhasználónévvel, vagy a jelszóval van-e baj, így csak általános hibát jelzünk
                ModelState.AddModelError("", "Hibás felhasználónév-jelszó páros.");
                user.IsValid = false;
                return View("LoginAdvertiser", user);
            }

            // ha sikeres volt az ellenőrzés
            HttpContext.Session.SetString("advertiser", user.Username); // felvesszük a felhasználó nevét a munkamenetbe

            return RedirectToAction("Index", "Home", new { login = true }); // átirányítjuk a főoldalra
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            feedMenu();
            if (HttpContext.Session.GetString("user") != null)
            {
                // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
                HttpContext.Session.Remove("user");
            }

            if (HttpContext.Session.GetString("advertiser") != null)
            {
                // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
                HttpContext.Session.Remove("advertiser");
            }

            return RedirectToAction("Index", "Home", new { logout = true }); // átirányítjuk a főoldalra
        }
    }
}
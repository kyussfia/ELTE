using ELTE.TravelAgency.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELTE.TravelAgency.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private ITravelService _travelService;

        public AccountController()
        {
            _accountService = new AccountService();
            _travelService = new TravelService();

            ViewBag.Cities = _travelService.Cities.ToList();
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="user">A bejelentkezés adatai.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Home", user);

            // bejelentkeztetjük a felhasználót
            if (!_accountService.Login(user))
            {
                // nem szeretnénk, ha a felhasználó tudná, hogy a felhasználónévvel, vagy a jelszóval van-e baj, így csak általános hibát jelzünk
                ModelState.AddModelError("", "Hibás felhasználónév, vagy jelszó.");
                return View("Login", user);
            }

            // ha sikeres volt az ellenőrzés

            Session["user"] = user.UserName; // felvesszük a felhasználó nevét a munkamenetbe
            Session.Timeout = 15; // max. 15 percig él a munkamenet
                    
            return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }

        /// <summary>
        /// Regisztráció.
        /// </summary>
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        /// <summary>
        /// Regisztráció.
        /// </summary>
        /// <param name="guest">Regisztrációs adatok.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(GuestRegistrationViewModel guest)
        {
            // végrehajtjuk az ellenőrzéseket
            if (!ModelState.IsValid)
                return View("Register", guest);

            if (!_accountService.Register(guest))
            {
                ModelState.AddModelError("UserName", "A megadott felhasználónév már létezik.");
                return View("Register", guest);
            }

            ViewBag.Information = "A regisztráció sikeres volt. Kérjük, jelentkezzen be.";

            if (Session["user"] != null) // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
                Session.Remove("user");

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        public ActionResult Logout()
        {
            if (Session["user"] != null) // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
                Session.Remove("user");

            return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }
    }
}
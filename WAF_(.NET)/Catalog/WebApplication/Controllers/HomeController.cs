using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;


namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationContext cont;

        public HomeController(ApplicationContext em)
        {
            cont = em;
        }

        public IActionResult Index()
        {
            ViewBag.Animals = cont.Animals.OrderByDescending(i => i.CreatedAt).ToList();
            return View("Index");
        }

        public IActionResult IndexASP()
        {
            return View("IndexASP");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult NewAd()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                var vm = new NewLoginViewModel();
                vm.Species = new List<string>
                {
                    "Kutya",
                    "Macska"
                };
                return View("NewAd", vm);
            }
            return NotFound("Unauthorized error.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewAd(NewLoginViewModel form)
        {
            if (form.Species == null)
            {
                form.Species = new List<string>
                {
                    "Kutya",
                    "Macska"
                };
            }
            if (HttpContext.Session.GetString("user") != null)
            {
                if (!ModelState.IsValid)
                {
                    //ModelState.AddModelError("Price", "Your bid price must be greater than the current price.");
                    return View("NewAd", form);
                }

                if (form.CreatedAt < DateTime.Now)
                {
                    ModelState.AddModelError("CreatedAt", "Created can only be greter than or equal today.");
                    return View("NewAd", form);
                }

                if (!create(form))
                {
                    ModelState.AddModelError("Spec", "Your bid price must be greater than the current price.");
                    return View("NewAd", form);
                }

                return RedirectToAction("Index");
            }
            return NotFound("Unauthorized error.");
        }

        public static string RandomString(int length)
        {
             Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected Boolean create(NewLoginViewModel ad)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                User guest = cont.Users.FirstOrDefault(c => c.Name == HttpContext.Session.GetString("user"));

               
                var log = new Login
                {
                    CreatedAt = ad.CreatedAt,
                    Place = ad.Place,
                    Spec = ad.Spec,
                    UserId = guest.Id
                };

                cont.Logins.Add(log);
                cont.SaveChanges();

                return true;
            }
            return false;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hibás felhasználónév-jelszó páros.");
                user.IsValid = false;
                return View("Login", user);
            }

            if (!LoginCustomer(user))
            {
                // nem szeretnénk, ha a felhasználó tudná, hogy a felhasználónévvel, vagy a jelszóval van-e baj, így csak általános hibát jelzünk
                ModelState.AddModelError("", "Hibás felhasználónév-jelszó páros.");
                user.IsValid = false;
                return View("Login", user);
            }

            // ha sikeres volt az ellenőrzés
            HttpContext.Session.SetString("user", user.Username); // felvesszük a felhasználó nevét a munkamenetbe

            return RedirectToAction("Index", "Home", new { login = true }); // átirányítjuk a főoldalra
        }

        public Boolean LoginCustomer(LoginViewModel user)
        {
            if (user == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(user, new ValidationContext(user, null, null), null))
                return false;

            User guest = cont.Users.FirstOrDefault(c => c.Name == user.Username);

            if (guest == null)
                return false;

            return checkPassword(user.Password, guest.Password);
        }

        protected Boolean checkPassword(string checkable, byte[] control)
        {
            // ellenőrizzük a jelszót (ehhez a kapott jelszót hash-eljük)
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(checkable));
            }

            if (!passwordBytes.SequenceEqual(control))
            {
                return false;
            }

            return true;
        }
    }
}

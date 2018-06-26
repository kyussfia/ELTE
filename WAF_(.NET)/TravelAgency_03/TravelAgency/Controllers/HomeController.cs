using ELTE.TravelAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ELTE.TravelAgency.Controllers
{
    /// <summary>
    /// Alap vezérlő típusa.
    /// </summary>
    public class HomeController : Controller
    {
        // a logikát egy modell osztály mögé rejtjük
        private ITravelService _travelService;
        
        /// <summary>
        /// Vezérlő példányosítása.
        /// </summary>
        public HomeController() 
        {
            _travelService = new TravelService();

            // minden lekérdezés a modellen keresztül történik
            ViewBag.Cities = _travelService.Cities.ToList();
        }

        /// <summary>
        /// Épületek listázása.
        /// </summary>
        /// <returns>Az épületek listájának nézete.</returns>
        public ActionResult Index()
        {
            return View("Index", _travelService.Buildings.ToList());
        }

        /// <summary>
        /// Épületek listázása.
        /// </summary>
        /// <param name="cityId">Város azonosítója.</param>
        /// <returns>Az épületek listájának nézete.</returns>
        public ActionResult List(Int32? cityId)
        {
            // minden lekérdezés a modellen keresztül történik
            List<Building> buildings = _travelService.GetBuildings(cityId).ToList();

            if (buildings == null) // ha nincs ilyen épület
                return RedirectToAction("Index"); // átirányítjuk a kezdőoldalra

            return View("Index", buildings);
        }

        /// <summary>
        /// Épület részleteinek nézete.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        /// <returns>Az épület részletes nézete.</returns>
        public ActionResult Details(Int32? buildingId)
        {
            Building building = _travelService.GetBuilding(buildingId);

            if (building == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Épület részletei: " + building.Name + " (" + building.City.Name + ")"; // az oldal címe
            ViewBag.Images = _travelService.GetBuildingImageIds(building.Id).ToList();

            return View("Details", building);
        }

        /// <summary>
        /// Épület főképének lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        /// <returns>Az épület képe, vagy az alapértelmezett kép.</returns>
        public FileResult ImageForBuilding(Int32? buildingId) 
        {
            Byte[] image = _travelService.GetBuildingMainImage(buildingId);

            if (image == null) // nem sikerült betölteni a képet
                return File("~/Content/NoImage.png", "image/png");

            return File(image, "image/png");
        }

        /// <summary>
        /// Épület egyik képének lekérdezése.
        /// </summary>
        /// <param name="imageId">Kép azonosítója.</param>
        /// <param name="isLarge">Nagy méretű kép lekérése.</param>
        /// <returns>Az épület egy képe, vagy az alapértelmezett kép.</returns>
        public FileResult Image(Int32? imageId, Boolean isLarge = false)
        {
            Byte[] image = _travelService.GetBuildingImage(imageId, isLarge);

            if (image == null) // nem sikerült betölteni a képet
                return File("~/Content/NoImage.png", "image/png");
            
            return File(image, "image/png");
        }
	}
}
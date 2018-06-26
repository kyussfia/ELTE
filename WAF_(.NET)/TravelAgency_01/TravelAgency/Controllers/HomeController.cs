using ELTE.TravelAgency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ELTE.TravelAgency.Controllers
{
	/// <summary>
	/// Vezérlő típusa
	/// </summary>
	public class HomeController : Controller
	{
		private TravelAgencyContext _context;

		/// <summary>
		/// Vezérlő példányosítása.
		/// </summary>
		public HomeController(TravelAgencyContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Épületek listázása.
		/// </summary>
		/// <returns>Az épületek listájának nézete.</returns>
		public IActionResult Index()
		{
			// a városokat berakjuk egy tömbbe
			ViewBag.Cities = _context.Cities.ToArray();

			return View("Index", _context.Buildings.Include(b => b.City));
		}


		/// <summary>
		/// Épületek listázása.
		/// </summary>
		/// <param name="cityId">Város azonosítója.</param>
		/// <returns>Az épületek listájának nézete.</returns>
		public IActionResult List(Int32 cityId)
		{
			// ha hibás az azonosító
			if (!_context.Cities.Any(c => c.Id == cityId))
				return NotFound(); // átirányítjuk a nem talált oldalra

			// a városokat berakjuk egy tömbbe
			ViewBag.Cities = _context.Cities.ToArray();

			// megkeressük a megfelelő város azonosítókat 
			return View("Index", _context.Buildings.Include(b => b.City).Where(b => b.CityId == cityId));
		}

		/// <summary>
		/// Épület részleteinek nézete.
		/// </summary>
		/// <param name="buildingId">Épület azonosítója.</param>
		/// <returns>Az épület részletes nézete.</returns>
		public IActionResult Details(Int32 buildingId)
		{
			Building building = _context.Buildings.Include(b => b.City).Include(b => b.Apartments).FirstOrDefault(b => b.Id == buildingId);

			if (building == null)
				return NotFound(); // átirányítjuk a nem talált oldalra

			// az oldal címe
			ViewBag.Title = "Épület részletei: " + building.Name + " (" + building.City.Name + ")";
			// a városokat berakjuk egy tömbbe
			ViewBag.Cities = _context.Cities.ToArray();

			return View("Details", building);
		}
	}
}
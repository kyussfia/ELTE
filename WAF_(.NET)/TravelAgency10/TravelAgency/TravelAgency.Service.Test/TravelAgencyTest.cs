using System;
using System.Collections.Generic;
using System.Linq;
using ELTE.TravelAgency.Data;
using ELTE.TravelAgency.Service.Controllers;
using ELTE.TravelAgency.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ELTE.TravelAgency.Service.Test
{
	public class TravelAgencyTest : IDisposable
	{
		private readonly TravelAgencyContext _context;
		private readonly List<CityDTO> _cityDTOs;
		private readonly List<BuildingDTO> _buildingDTOs;

		public TravelAgencyTest()
		{
			var options = new DbContextOptionsBuilder<TravelAgencyContext>()
				.UseInMemoryDatabase("TravelAgencyTest")
				.Options;

			_context = new TravelAgencyContext(options);
			_context.Database.EnsureCreated();

			// adatok inicializációja
			var cityData = new List<City>
			{
				new City { Name = "TESTCITY" }
			};
			_context.Cities.AddRange(cityData);

			var buildingData = new List<Building>
			{
				new Building { City = cityData[0], Name = "TESTBUILDING1", SeaDistance = 1, ShoreId = ShoreType.Rocky},
				new Building { City = cityData[0], Name = "TESTBUILDING2", SeaDistance = 10, ShoreId = ShoreType.Gravelly },
				new Building { City = cityData[0], Name = "TESTBUILDING3", SeaDistance = 100, ShoreId = ShoreType.Sandy }
			};
			_context.Buildings.AddRange(buildingData);
			_context.SaveChanges();

			_cityDTOs = cityData.Select(city => new CityDTO
			{
				Id = city.Id,
				Name = city.Name
			}).ToList();

			_buildingDTOs = buildingData.Select(building => new BuildingDTO
			{
				Id = building.Id,
				Name = building.Name,
				City = _cityDTOs.Single(city => city.Id == building.CityId),
				Comment = building.Comment,
				Images = new List<ImageDTO>(),
				LocationX = building.LocationX,
				LocationY = building.LocationY,
				SeaDistance = building.SeaDistance,
				ShoreId = building.ShoreId
			}).ToList();
		}

		public void Dispose()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[Fact]
		public void GetCityTest()
		{
			var controller = new CitiesController(_context);
			var result = controller.GetCities();

			// Assert
			var objectResult = Assert.IsType<OkObjectResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<CityDTO>>(objectResult.Value);
			Assert.Equal(_cityDTOs, model);
		}

		[Fact]
		public void GetBuildingTest()
		{
			var controller = new BuildingsController(_context);
			var result = controller.GetBuildings();

			// Assert
			var objectResult = Assert.IsType<OkObjectResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<BuildingDTO>>(objectResult.Value);
			Assert.Equal(_buildingDTOs, model);
		}

		[Fact]
		public void CreateBuildingTest()
		{
			var newBuilding = new BuildingDTO
			{
				City = _cityDTOs[0],
				Name = "TESTBUILDING4",
				SeaDistance = 1000,
				ShoreId = ShoreType.Rocky
			};

			var controller = new BuildingsController(_context);
			var result = controller.PostBuilding(newBuilding);

			// Assert
			var objectResult = Assert.IsType<CreatedAtRouteResult>(result);
			var model = Assert.IsAssignableFrom<BuildingDTO>(objectResult.Value);
			Assert.Equal(_buildingDTOs.Count + 1, _context.Buildings.Count());
			Assert.Equal(newBuilding, model);
		}
	}
}

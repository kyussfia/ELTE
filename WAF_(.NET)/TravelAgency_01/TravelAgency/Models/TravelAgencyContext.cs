﻿using Microsoft.EntityFrameworkCore;

namespace ELTE.TravelAgency.Models
{
	public class TravelAgencyContext : DbContext
	{
		public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options)
			: base(options)
		{
		}

		public DbSet<Apartment> Apartments { get; set; }
		public DbSet<Building> Buildings { get; set; }
		public DbSet<City> Cities { get; set; }
	}
}

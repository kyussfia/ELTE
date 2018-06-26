﻿using System.Collections.Generic;

namespace ELTE.TravelAgency.Models
{
	public class City
	{
		public City()
		{
			Buildings = new HashSet<Building>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Building> Buildings { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}

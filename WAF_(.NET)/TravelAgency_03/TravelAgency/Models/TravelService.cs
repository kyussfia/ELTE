using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ELTE.TravelAgency.Models
{
    /// <summary>
    /// Az utazással kapcsolatos szolgáltatások típusa.
    /// </summary>
    public class TravelService : ITravelService
    {
        private TravelAgencyEntities _entities;

        public TravelService()
        {
            _entities = new TravelAgencyEntities();
        }
        
        /// <summary>
        /// Városok lekérdezése.
        /// </summary>
        public IEnumerable<Building> Buildings
        {
            get
            {
                // betöltjük az épületek mellett a városok adatait is
                return _entities.Building.Include("City");
            }
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        public IEnumerable<City> Cities
        {
            get
            {
                return _entities.City;
            }
        }

        /// <summary>
        /// Épület lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public Building GetBuilding(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            return _entities.Building.FirstOrDefault(building => building.Id == buildingId);
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        /// <param name="cityId">Város azonosítója.</param>
        public IEnumerable<Building> GetBuildings(Int32? cityId)
        {
            if (cityId == null || !_entities.Building.Any(building => building.CityId == cityId))
                return null;

            return _entities.Building.Where(building => building.CityId == cityId);
        }

        /// <summary>
        /// Épület lekérdezése apartmanokkal.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public Building GetBuildingWithApartments(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            return _entities.Building.Include("City").Include("Apartment").FirstOrDefault(b => b.Id == buildingId);
        }

        /// <summary>
        /// Épület képek azonosítóinak lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public IEnumerable<Int32> GetBuildingImageIds(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            return _entities.BuildingImage.Where(image => image.BuildingId == buildingId).Select(image => image.Id);
        }

        /// <summary>
        /// Épület főképének lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public Byte[] GetBuildingMainImage(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            // lekérjük az épület első tárolt képjét (kicsiben)
            return _entities.BuildingImage.Where(image => image.BuildingId == buildingId).Select(image => image.ImageSmall).FirstOrDefault();
        }

        /// <summary>
        /// Épület képének lekérdezése.
        /// </summary>
        /// <param name="imageId">Kép azonosítója.</param>
        /// <param name="large">Nagy kép letöltése.</param>
        public Byte[] GetBuildingImage(Int32? imageId, Boolean large)
        {
            if (imageId == null)
                return null;

            // lekérjük az épület első tárolt képjét (kicsiben)
            BuildingImage image = _entities.BuildingImage.FirstOrDefault(im => im.Id == imageId);

            if (image == null)
                return null;

            if (large)
                return image.ImageLarge;
            else
                return image.ImageSmall;
        }

        /// <summary>
        /// Apartman lekérdezése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        public Apartment GetApartment(Int32? apartmentId)
        {
            if (apartmentId == null)
                return null;

            return _entities.Apartment.FirstOrDefault(apartment => apartment.Id == apartmentId);
        }

        /// <summary>
        /// Foglalás létrehozása.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        public RentViewModel NewRent(Int32? apartmentId)
        {
            if (apartmentId == null)
                return null;

            Apartment apartment = _entities.Apartment.Include("Building").Include("Building.City").FirstOrDefault(ap => ap.Id == apartmentId);

            RentViewModel rent = new RentViewModel { Apartment = apartment }; // létrehozunk egy új foglalást, amelynek megadjuk az apartmant

            // beállítunk egy foglalást, amely a következő megfelelő fordulónappal (minimum 1 héttel később), és egy hetes időtartammal rendelkezik
            rent.RentStartDate = DateTime.Today + TimeSpan.FromDays(7);
            while (rent.RentStartDate.DayOfWeek != apartment.TurndayOfWeek)
                rent.RentStartDate += TimeSpan.FromDays(1);

            rent.RentEndDate = rent.RentStartDate + TimeSpan.FromDays(7);

            return rent;
        }

        /// <summary>
        /// Foglalás árának lekérdezése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        /// <param name="rent">Foglalás adatai.</param>
        public Int32 GetPrice(Int32? apartmentId, RentViewModel rent)
        {
            if (apartmentId == null || rent == null || rent.Apartment == null)
                return 0;

            return rent.Apartment.Price * Convert.ToInt32((rent.RentEndDate - rent.RentStartDate).TotalDays);
        }
        
        /// <summary>
        /// Foglalás mentése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        /// <param name="rent">Foglalás adatai.</param>
        /// <returns>Sikeres volt-e a foglalás.</returns>
        public Boolean SaveRent(Int32? apartmentId, RentViewModel rent)
        {
            if (apartmentId == null || rent == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(rent, new ValidationContext(rent, null, null), null))
                return false;

            // ellenőrizzük a dátumot
            if (RentDateValidator.Validate(rent.RentStartDate, rent.RentEndDate, apartmentId.Value) != RentDateError.None)
                return false;
            
            // átalakítjuk az adatokat
            Guest guest = new Guest
            {
                UserName = "user" + Guid.NewGuid(), // a felhasználónevet generáljuk
                Name = rent.GuestName,
                Email = rent.GuestEmail,
                Address = rent.GuestAddress,
                PhoneNumber = rent.GuestPhoneNumber
            };
            _entities.Guest.Add(guest);

            _entities.Rent.Add(new Rent
            {
                ApartmentId = rent.Apartment.Id,
                UserName = guest.UserName,
                StartDate = rent.RentStartDate,
                EndDate = rent.RentEndDate
            });

            try
            {
                _entities.SaveChanges();
            }
            catch(Exception ex)
            {
                // mentéskor lehet hiba
                return false;
            }

            // ha idáig eljuttottunk, minden sikeres volt
            return true;
        }
    }
}
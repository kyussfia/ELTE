using System;
using System.Linq;

namespace ELTE.TravelAgency.Models
{
    /// <summary>
    /// Foglalás dátumát ellenőrző típus.
    /// </summary>
    public static class RentDateValidator
    {
        /// <summary>
        /// Foglalás dátumainak ellenőrzése.
        /// </summary>
        /// <param name="start">Foglalás kezdete.</param>
        /// <param name="end">Foglalás vége.</param>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        public static RentDateError Validate(DateTime start, DateTime end, Int32 apartmentId)
        {
            if (start < DateTime.Now + TimeSpan.FromDays(7)) // korai kezdés
                return RentDateError.StartInvalid;

            if (end < start)
                return RentDateError.EndInvalid;

            if (end == start) // üres foglalás 
                return RentDateError.LengthInvalid;

            if (Convert.ToInt32((end - end).TotalDays) % 7 != 0) // nem egész hetet foglalt
                return RentDateError.LengthInvalid;

            using (TravelAgencyEntities entities = new TravelAgencyEntities())
            {
                Apartment selectedApartment = entities.Apartment.FirstOrDefault(apartment => apartment.Id == apartmentId);

                if (selectedApartment == null)
                    return RentDateError.None;

                if (start.DayOfWeek != selectedApartment.TurndayOfWeek) // nem fordulónapos kezdés
                    return RentDateError.StartInvalid;

                if (entities.Rent.Where(r => r.ApartmentId == selectedApartment.Id && r.EndDate >= start)
                                 .ToList()
                                 .Any(r => r.IsConflicting(start, end))) // az időszakra már van foglalás
                    return RentDateError.Conflicting;
            }

            return RentDateError.None; // ha ideág eljutunk, nem találtunk hibát.
        }
    }
}
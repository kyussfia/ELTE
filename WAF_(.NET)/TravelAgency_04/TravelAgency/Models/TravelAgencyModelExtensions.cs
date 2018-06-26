using System;
using System.ComponentModel.DataAnnotations;

namespace ELTE.TravelAgency.Models
{
    /// <summary>
    /// Apartman (kiegészítések).
    /// </summary>
    public partial class Apartment
    {
        /// <summary>
        /// A váltás napja.
        /// </summary>
        [UIHint("DayOfWeekDisplay")] // megadjuk a megjelenítés módját
        public DayOfWeek TurndayOfWeek { get { return (DayOfWeek)(Turnday % 7); } }
    }

    /// <summary>
    /// Épület (kiegészítések).
    /// </summary>
    public partial class Building
    {
        /// <summary>
        /// Tengerpart típus.
        /// </summary>
        [UIHint("ShoreTypeDisplay")] // megadjuk a megjelenítés módját
        public Int32? ShoreType { get { return ShoreId; } }
    }

    /// <summary>
    /// Foglalás (kiegészítések).
    /// </summary>
    public partial class Rent
    {
        /// <summary>
        /// Ütközik-e egy másik foglalással.
        /// </summary>
        /// <param name="startDate">A foglalás kezdete.</param>
        /// <param name="endDate">A foglalás vége.</param>
        /// <returns>Igaz, ha ütközik, egyébként hamis.</returns>
        public Boolean IsConflicting(DateTime startDate, DateTime endDate)
        {
            return StartDate >= startDate && StartDate < endDate ||
                   EndDate >= startDate && EndDate < endDate ||
                   StartDate < startDate && EndDate > endDate ||
                   StartDate > startDate && EndDate < endDate;
        }
    }
}
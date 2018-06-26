using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models.ViewModel
{
    public class RegisterAdvertiserViewModel : LoginViewModel
    {
        [Required(ErrorMessage = "A név megadása kötelező.")] // feltételek a validáláshoz
        [StringLength(60, ErrorMessage = "A foglaló neve maximum 60 karakter lehet.")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A jelszó ismételt megadása kötelező.")]
        [Compare("Password", ErrorMessage = "A két jelszó nem egyezik.")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
    }
}

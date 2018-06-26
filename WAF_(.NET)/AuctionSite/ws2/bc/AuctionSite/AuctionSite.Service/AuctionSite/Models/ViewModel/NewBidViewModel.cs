using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuctionSite.Models.ViewModel
{
    public class NewBidViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Cannot make new bid without Price.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Price can be only a numeric value.")]
        public Int32 Price { get; set; }
    }
}

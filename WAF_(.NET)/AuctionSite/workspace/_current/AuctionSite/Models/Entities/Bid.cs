using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSite.Models.Entities
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}

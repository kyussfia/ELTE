using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Models.Entities
{
    public class Advertiser
    {
        public Advertiser()
        {
            Items = new List<Item>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public byte[] Password { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}

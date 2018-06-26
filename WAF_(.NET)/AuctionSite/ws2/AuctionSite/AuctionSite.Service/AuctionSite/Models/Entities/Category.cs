using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AuctionSite.Models.Entities
{
    public class Category
    {
        public Category()
        {
            Items = new List<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}

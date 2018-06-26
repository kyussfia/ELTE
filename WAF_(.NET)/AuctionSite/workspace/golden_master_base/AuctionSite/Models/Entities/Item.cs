using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSite.Models.Entities
{
    public class Item
    {
        public Item()
        {
            Bids = new List<Bid>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int OriginalBid { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        public DateTime ClosedAt { get; set; }

        public Byte[] Picture { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AdvertiserId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Advertiser Advertiser { get; set; }

        public ICollection<Bid> Bids { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual IList<Bid> BidHistory
        {
            get
            {
                return Bids.OrderByDescending(b => b.CreatedAt).ToList();
            }
        }

        public virtual bool HasBid
        {
            get
            {
                return Bids.ToList().Count > 0;
            }
        }

        public virtual Bid TopBid
        {
            get
            {
                return Bids.OrderByDescending(b => b.CreatedAt).First();
            }
        }
    }
}

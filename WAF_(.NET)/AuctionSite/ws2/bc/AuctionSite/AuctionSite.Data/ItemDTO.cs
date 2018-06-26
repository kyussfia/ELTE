using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionSite.Data
{
    public class ItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OriginalBid { get; set; }

        public string Currency { get; set; }

        public DateTime ClosedAt { get; set; }

        public Byte[] Picture { get; set; }

        public IList<Image> Images { get { var list = new List<Image>(); list.Add(new Image { Data = Picture } ); return list; } }

        public int CategoryId { get; set; }

        public int AdvertiserId { get; set; }

        public CategoryDTO Category { get; set; }

        public virtual Boolean IsClosed { get { return ClosedAt <= DateTime.Now; } }

        public virtual String CategoryName { get { return Category == null ? "" :  Category.Name; } }

        public virtual Boolean HasBid { get { return Bids == null ? false : Bids.Count > 0; } }

        public virtual Boolean HasImage { get { return Picture != null; } }

        public ICollection<BidDTO> Bids { get; set; }

        public BidDTO TopBid { get { return Bids == null ? null :Bids.OrderByDescending(b => b.CreatedAt).FirstOrDefault(); } }

        public virtual int? TopBidPrice { get { return TopBid == null ? null : (int?)TopBid.Price; } }

        public virtual int NumOfBids { get { return HasBid ? Bids.Count : 0; } }

        public DateTime CreatedAt { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is ItemDTO dto) && Id == dto.Id && Name == dto.Name && CategoryId == dto.CategoryId && AdvertiserId == dto.AdvertiserId && ClosedAt == dto.ClosedAt && Currency == dto.Currency;
        }

    }
}

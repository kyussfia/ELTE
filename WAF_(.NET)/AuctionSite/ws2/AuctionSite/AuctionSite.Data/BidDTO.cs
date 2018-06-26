using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSite.Data
{
    public class BidDTO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ItemDTO Item { get; set; }
        public UserDTO User { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is BidDTO dto) && Id == dto.Id;
        }
    }
}

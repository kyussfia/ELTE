using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSite.Data
{
    public class AdvertiserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is AdvertiserDTO dto) && Id == dto.Id;
        }
    }
}

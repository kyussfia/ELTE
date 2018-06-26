using System;
using System.Collections.Generic;

namespace AuctionSite.Data
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is CategoryDTO dto) && Id == dto.Id && Name == dto.Name;
        }
    }
}

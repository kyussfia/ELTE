using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSite.Data
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is UserDTO dto) && Id == dto.Id && Name == dto.Name && PhoneNumber == dto.PhoneNumber && Username == dto.Username && Email == dto.Email;
        }
    }
}

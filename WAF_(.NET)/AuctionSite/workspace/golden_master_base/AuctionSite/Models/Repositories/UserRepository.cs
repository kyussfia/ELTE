using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AuctionSite.Models.Repositories
{
    public class UserRepository : BaseEntityRepo
    {
        public UserRepository(Db.AuctionContext cont)
        {
            context = cont;
        }

        public Boolean Login(ViewModel.Login user)
        {
            if (user == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(user, new ValidationContext(user, null, null), null))
                return false;

            Entities.User guest = GetUserByUsername(user.UserName);

            if (guest == null)
                return false;

            // ellenőrizzük a jelszót (ehhez a kapott jelszót hash-eljük)
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
            }

            if (!passwordBytes.SequenceEqual(guest.Password))
                
                return false;

            return true;
        }

        public Entities.User GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault(c => c.Username == username);
        }
    }
}

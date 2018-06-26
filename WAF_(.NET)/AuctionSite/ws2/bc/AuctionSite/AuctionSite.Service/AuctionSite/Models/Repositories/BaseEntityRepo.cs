using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AuctionSite.Models.Repositories
{
    public class BaseEntityRepo
    {
        protected Db.AuctionContext context;

        public Boolean Logout()
        {
            return true;
        }

        protected Boolean validateViewModel(ViewModel.BaseViewModel viewModel)
        {
            if (viewModel == null)
            {
                return false;
            }

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(viewModel, new ValidationContext(viewModel, null, null), null))
            {
                return false;
            }

            return true;
        }

        protected byte[] codePassword(string pwd)
        {
            // kódoljuk a jelszót
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            }

            return passwordBytes;
        }

        protected Boolean checkPassword(string checkable, byte[] control)
        {
            // ellenőrizzük a jelszót (ehhez a kapott jelszót hash-eljük)
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(checkable));
            }

            if (!passwordBytes.SequenceEqual(control))
            {
                return false;
            }

            return true;
        }

        protected Boolean save()
        {
            try
            {
                context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}

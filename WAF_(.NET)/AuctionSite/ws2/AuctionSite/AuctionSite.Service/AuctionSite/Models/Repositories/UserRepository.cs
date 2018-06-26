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

        public Boolean LoginCustomer(ViewModel.LoginViewModel user)
        {
            if (user == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(user, new ValidationContext(user, null, null), null))
                return false;

            Entities.User guest = GetUserByUsername(user.Username);

            if (guest == null)
                return false;

            return checkPassword(user.Password, guest.Password);
        }


        public Entities.User GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault(c => c.Username == username);
        }

        public Boolean RegisterCustomer(ViewModel.RegisterCustomerViewModel viewModel)
        {
            if (!this.validateViewModel(viewModel) || context.Users.Count(c => c.Username == viewModel.Username) != 0)
            {
                return false;
            }

            context.Users.Add(new Entities.User
            {
                Name = viewModel.Name,
                Username = viewModel.Username,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                Password = codePassword(viewModel.Password)
            });

            return save();
        }
    }
}

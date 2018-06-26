using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AuctionSite.Models.Repositories
{
    public class AdvertiserRepository : BaseEntityRepo
    {
        public AdvertiserRepository(Db.AuctionContext cont)
        {
            context = cont;
        }

        public Boolean IsAdvertiserExist(Int32 advertiserId)
        {
            return context.Advertisers.Any(a => a.Id == advertiserId);
        }

        public Entities.Advertiser GetAdvertiser(Int32 advertiserId)
        {
            return context.Advertisers.Where(a => a.Id == advertiserId).Single();
        }

        public Entities.Advertiser GetAdvertiserByUsername(string username)
        {
            return context.Advertisers.FirstOrDefault(c => c.Username == username);
        }

        public Boolean RegisterAdvertiser(ViewModel.RegisterAdvertiserViewModel viewModel)
        {
            if (!validateViewModel(viewModel) || context.Advertisers.Count(c => c.Username == viewModel.Username) != 0)
            {
                return false;
            }

            context.Advertisers.Add(new Entities.Advertiser
            {
                Name = viewModel.Name,
                Username = viewModel.Username,
                Password = codePassword(viewModel.Password)
            });

            return save();
        }

        public Boolean LoginAdvertiser(ViewModel.LoginViewModel user)
        {
            if (user == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(user, new ValidationContext(user, null, null), null))
                return false;

            Entities.Advertiser guest = GetAdvertiserByUsername(user.Username);

            if (guest == null)
                return false;

            return checkPassword(user.Password, guest.Password);
        }
    }
}

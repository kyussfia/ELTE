using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionSite.Data;

namespace AuctionSite.Admin.Model
{
    public interface IAuctionSiteModel
    {
        IReadOnlyList<CategoryDTO> Categories { get; }

        IReadOnlyList<ItemDTO> Items { get; }

        Boolean IsUserLoggedIn { get; }

        event EventHandler<ItemEventArgs> ItemChanged;

        void CreateItem(ItemDTO item);

        //void CreateImage(Int32 buildingId, Byte[] imageSmall, Byte[] imageLarge);

        void UpdateItem(ItemDTO item);

        Task LoadAsync();
        Task SaveAsync();
        Task<Boolean> LoginAsync(String userName, String userPassword);
        Task<Boolean> LogoutAsync();
    }
}

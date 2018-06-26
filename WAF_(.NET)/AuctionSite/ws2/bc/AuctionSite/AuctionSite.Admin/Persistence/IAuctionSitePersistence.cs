using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionSite.Data;

namespace AuctionSite.Admin.Persistence
{
    public interface IAuctionSitePersistence
    {
        Task<IEnumerable<ItemDTO>> ReadItemsAsync(int advertId);

        Task<IEnumerable<CategoryDTO>> ReadCategoriesAsync();

        Task<Boolean> CreateItemAsync(ItemDTO item);

        Task<Boolean> UpdateItemAsync(ItemDTO item);

        Task<Boolean> LoginAsync(String userName, String userPassword); //as advertiser

        Task<Boolean> LogoutAsync();

        Task<AdvertiserDTO> ReadAdvertiserIdAsync(String username);
    }
}

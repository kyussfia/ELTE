using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuctionSite.Models
{
    public interface IAuctionInterface
    {
        /* Categories */
        IEnumerable<Entities.Category> GetCategories();
        Boolean IsCategoryExist(Int32 catId);
        Entities.Category GetCategory(Int32 categoryId);

        /* Items */
        Boolean IsItemExist(Int32? itemId);
        Entities.Item GetItem(Int32 itemId);
        Entities.Item GetItemOnly(Int32 itemId);
        IEnumerable<Entities.Item> GetValidItems();
        IQueryable<Models.Entities.Item> GetValidItemsByCategory(Int32 categoryId);
        IQueryable<Models.Entities.Item> GetValidItemsByCategoryFilteredByName(Int32 categoryId, string filter);
        IQueryable<Entities.Item> GetLast20ActiveItems();
        IQueryable<Models.Entities.Item> GetValidItemsByAdvertiser(Int32 advertiserId);
        IQueryable<Models.Entities.Item> GetValidItemsByAdvertiserFilteredByName(Int32 advertiserId, string filter);
        IQueryable<Models.Entities.Item> GetItemsBiddedByUser(Entities.User user);
        IQueryable<Models.Entities.Item> GetItemsBiddedByUserFilteredByName(Entities.User user, string filter);

        /* Users */
        Boolean RegisterCustomer(Models.ViewModel.RegisterCustomerViewModel viewModel);
        Boolean LoginCustomer(Models.ViewModel.LoginViewModel viewModel);
        Entities.User GetUserByUsername(string username);

        /* Advertisers */
        Boolean RegisterAdvertiser(Models.ViewModel.RegisterAdvertiserViewModel viewModel);
        Boolean LoginAdvertiser(Models.ViewModel.LoginViewModel viewModel);
        Entities.Advertiser GetAdvertiserByUsername(string username);
        Boolean IsAdvertiserExist(Int32 advertiserId);
        Entities.Advertiser GetAdvertiser(Int32 advertiserId);

        /* Bids */
        Boolean createBid(Entities.Item item, Entities.User by, Models.ViewModel.NewBidViewModel viewModel);
    }
}

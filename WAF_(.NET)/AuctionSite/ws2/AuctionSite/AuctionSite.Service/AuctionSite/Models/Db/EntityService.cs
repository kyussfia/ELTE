using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionSite.Models;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models.Repositories;

namespace AuctionSite.Models.Db
{
    public class EntityService : IAuctionInterface
    {
        private CategoryRepository categoryRepo;

        private ItemRepository itemRepo;

        private UserRepository userRepo;

        private AdvertiserRepository advertiserRepo;

        private BidRepository bidRepo;

        public EntityService(Db.AuctionContext cont)
        {
            categoryRepo = new CategoryRepository(cont);
            itemRepo = new ItemRepository(cont);
            userRepo = new UserRepository(cont);
            advertiserRepo = new AdvertiserRepository(cont);
            bidRepo = new BidRepository(cont);
        }

        /* Categories */
        public IEnumerable<Entities.Category> GetCategories() { return categoryRepo.GetCategories();  }
        public bool IsCategoryExist(Int32 catId) { return categoryRepo.IsCategoryExist(catId); }
        public Entities.Category GetCategory(Int32 categoryId) { return categoryRepo.GetCategory(categoryId); }

        /* Items */
        public bool IsItemExist(Int32? itemId) { return itemRepo.IsItemExist(itemId); }
        public Entities.Item GetItem(Int32 itemId) { return itemRepo.GetItem(itemId); }
        public Entities.Item GetItemOnly(Int32 itemId) { return itemRepo.GetItemOnly(itemId); }
        public IEnumerable<Models.Entities.Item> GetValidItems() { return itemRepo.GetValidItems(); }
        public IQueryable<Models.Entities.Item> GetValidItemsByCategory(Int32 categoryId) { return itemRepo.GetValidItemsByCategory(categoryId); }
        public IQueryable<Models.Entities.Item> GetValidItemsByCategoryFilteredByName(Int32 categoryId, string filter) { return itemRepo.GetValidItemsByCategoryFilteredByName(categoryId, filter); }
        public IQueryable<Entities.Item> GetLast20ActiveItems() { return itemRepo.GetLast20ActiveItems(); }
        public IQueryable<Models.Entities.Item> GetValidItemsByAdvertiser(Int32 advertiserId) { return itemRepo.GetValidItemsByAdvertiser(advertiserId); }
        public IQueryable<Models.Entities.Item> GetValidItemsByAdvertiserFilteredByName(Int32 advertiserId, string filter) { return itemRepo.GetValidItemsByAdvertiserFilteredByName(advertiserId, filter); }
        public IQueryable<Models.Entities.Item> GetItemsBiddedByUser(Entities.User user) { return itemRepo.GetItemsBiddedByUser(user); }
        public IQueryable<Models.Entities.Item> GetItemsBiddedByUserFilteredByName(Entities.User user, string filter) { return itemRepo.GetItemsBiddedByUserFilteredByName(user, filter); }
        public IEnumerable<Models.Entities.Item> GetItems() { return itemRepo.GetItems(); }

        /* Advertisers */
        public Boolean RegisterAdvertiser(Models.ViewModel.RegisterAdvertiserViewModel viewModel) { return advertiserRepo.RegisterAdvertiser(viewModel); }
        public Boolean LoginAdvertiser(Models.ViewModel.LoginViewModel viewModel) { return advertiserRepo.LoginAdvertiser(viewModel); }
        public Entities.Advertiser GetAdvertiserByUsername(string username) { return advertiserRepo.GetAdvertiserByUsername(username); }
        public Boolean IsAdvertiserExist(Int32 advertiserId) { return advertiserRepo.IsAdvertiserExist(advertiserId); }
        public Entities.Advertiser GetAdvertiser(Int32 advertiserId) { return advertiserRepo.GetAdvertiser(advertiserId); }
        public Task<Boolean> LoginAdvertiserPwd(String username, String pwd) { return advertiserRepo.LoginAdvertiserPwd(username, pwd); }
        public Task<Boolean> Logout() { return advertiserRepo.LogOut(); }
       

        /* Users */
        public Boolean LoginCustomer(Models.ViewModel.LoginViewModel viewModel) { return userRepo.LoginCustomer(viewModel); }
        public Boolean RegisterCustomer(Models.ViewModel.RegisterCustomerViewModel viewModel) { return userRepo.RegisterCustomer(viewModel); }
        public Entities.User GetUserByUsername(string username) { return userRepo.GetUserByUsername(username); }

        /* Bids */
        public Boolean createBid(Entities.Item item, Entities.User by, Models.ViewModel.NewBidViewModel viewModel) { return bidRepo.createBid(item, by, viewModel); }
    }
}

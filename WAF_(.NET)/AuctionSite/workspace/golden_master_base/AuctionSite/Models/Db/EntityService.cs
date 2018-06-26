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

        public EntityService(Db.AuctionContext cont)
        {
            categoryRepo = new CategoryRepository(cont);
            itemRepo = new ItemRepository(cont);
            userRepo = new UserRepository(cont);
            advertiserRepo = new AdvertiserRepository(cont);
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
    }
}

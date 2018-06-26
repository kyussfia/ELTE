using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuctionSite.Models.Repositories
{
    public class ItemRepository : BaseEntityRepo
    {
        public ItemRepository(Db.AuctionContext cont)
        {
            context = cont;
        }

        public Boolean IsItemExist(Int32? itemId)
        {
            return context.Items.Any(c => c.Id == itemId);
        }

        public Entities.Item GetItem(Int32 itemId)
        {
            return GetItems().Single(i => i.Id == itemId);
        }

        public Entities.Item GetItemOnly(Int32 itemId)
        {
            return context.Items.Single(i => i.Id == itemId);
        }

        private IEnumerable<Models.Entities.Item> GetItems()
        {
            return context.Items.Include(i => i.Advertiser).Include(i => i.Bids).Include(i => i.Category).Include(i => i.Bids).ThenInclude(b => b.User);
        }

        public IEnumerable<Models.Entities.Item> GetValidItems()
        {
            return GetItems().Where(i => i.ClosedAt > DateTime.Now);
        }

        public IQueryable<Models.Entities.Item> GetValidItemsByCategory(Int32 categoryId)
        {
            return GetValidItems().AsQueryable().Where(i => i.CategoryId == categoryId);
        }

        public IQueryable<Models.Entities.Item> GetValidItemsByCategoryFilteredByName(Int32 categoryId, string filter)
        {
            return GetValidItemsByCategory(categoryId).Where(i => i.Name.ToUpper().Contains(filter.ToUpper()));
        }

        public IQueryable<Models.Entities.Item> GetValidItemsByAdvertiser(Int32 advertiserId)
        {
            return GetValidItems().AsQueryable().Where(i => i.AdvertiserId == advertiserId);
        }

        public IQueryable<Models.Entities.Item> GetValidItemsByAdvertiserFilteredByName(Int32 advertiserId, string filter)
        {
            return GetValidItemsByAdvertiser(advertiserId).Where(i => i.Name.ToUpper().Contains(filter.ToUpper()));
        }

        public IQueryable<Entities.Item> GetLast20ActiveItems()
        {
            return GetValidItems().AsQueryable().OrderByDescending(i => i.CreatedAt).Take(20);
        }

        public IQueryable<Models.Entities.Item> GetItemsBiddedByUserFilteredByName(Entities.User user, string filter)
        {
            return GetItemsBiddedByUser(user).Where(i => i.Name.ToUpper().Contains(filter.ToUpper()));
        }

        public IQueryable<Models.Entities.Item> GetItemsBiddedByUser(Entities.User user)
        {
            return GetItems().AsQueryable().Where(i => GetUserbiddedItemIds(user).ToArray().Contains(i.Id));
        }

        private IEnumerable<int> GetUserbiddedItemIds(Entities.User user)
        {
            return GetUserBids(user).Select(b => b.ItemId);
        }

        private IEnumerable<Entities.Bid> GetUserBids(Entities.User user)
        {
            return context.Bids.Include(b => b.Item).Where(b => b.UserId == user.Id).OrderByDescending(b => b.CreatedAt);
        }        
    }
}

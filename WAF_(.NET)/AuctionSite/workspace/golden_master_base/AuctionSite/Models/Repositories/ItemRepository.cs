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

        public bool IsItemExist(Int32? itemId)
        {
            return context.Items.Any(c => c.Id == itemId);
        }

        public Entities.Item GetItem(Int32 itemId)
        {
            return context.Items.Include(i => i.Bids).ThenInclude(b => b.User).Single(i => i.Id == itemId);
        }

        public Entities.Item GetItemOnly(Int32 itemId)
        {
            return context.Items.Single(i => i.Id == itemId);
        }

        public IEnumerable<Models.Entities.Item> GetValidItems()
        {
            return context.Items.Include(i => i.Advertiser).Include(i => i.Bids).Where(i => i.ClosedAt > DateTime.Now);
        }

        public IQueryable<Models.Entities.Item> GetValidItemsByCategory(Int32 categoryId)
        {
            return GetValidItems().AsQueryable().Where(i => i.CategoryId == categoryId);
        }

        public IQueryable<Models.Entities.Item> GetValidItemsByCategoryFilteredByName(Int32 categoryId, string filter)
        {
            return GetValidItemsByCategory(categoryId).Where(i => i.Name.Contains(filter));
        }

        public IQueryable<Entities.Item> GetLast20ActiveItems()
        {
            return context.Items.Include(i => i.Advertiser).Include(i => i.Bids).Where(i => i.ClosedAt > DateTime.Now).OrderByDescending(i => i.CreatedAt).Take(20);
        }
    }
}

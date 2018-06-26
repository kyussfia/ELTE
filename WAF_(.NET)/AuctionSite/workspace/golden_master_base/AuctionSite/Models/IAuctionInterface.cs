using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuctionSite.Models
{
    public interface IAuctionInterface
    {
        IEnumerable<Entities.Category> GetCategories();
        bool IsCategoryExist(Int32 catId);
        Entities.Category GetCategory(Int32 categoryId);


        bool IsItemExist(Int32? itemId);
        Entities.Item GetItem(Int32 itemId);
        Entities.Item GetItemOnly(Int32 itemId);
        IEnumerable<Entities.Item> GetValidItems();
        IQueryable<Models.Entities.Item> GetValidItemsByCategory(Int32 categoryId);
        IQueryable<Models.Entities.Item> GetValidItemsByCategoryFilteredByName(Int32 categoryId, string filter);
        IQueryable<Entities.Item> GetLast20ActiveItems();
    }
}

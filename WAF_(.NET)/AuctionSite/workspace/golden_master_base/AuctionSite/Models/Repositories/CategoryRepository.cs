using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionSite.Models;

namespace AuctionSite.Models.Repositories
{
    public class CategoryRepository : BaseEntityRepo
    {
        public CategoryRepository(Db.AuctionContext cont)
        {
            context = cont;
        }

        public IEnumerable<Entities.Category> GetCategories()
        {
            return context.Categories;
        }

        public bool IsCategoryExist(Int32 catId)
        {
            return context.Categories.Any(c => c.Id == catId);
        }

        public Entities.Category GetCategory(Int32 categoryId)
        {
            return context.Categories.Where(c => c.Id == categoryId).Single();
        }
    }
}

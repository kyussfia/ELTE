using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSite.Models.Repositories
{
    public class AdvertiserRepository : BaseEntityRepo
    {
        public AdvertiserRepository(Db.AuctionContext cont)
        {
            context = cont;
        }
    }
}

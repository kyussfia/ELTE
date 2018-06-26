using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuctionSite.Models.Repositories
{
    public class BidRepository : BaseEntityRepo
    {
        public BidRepository(Db.AuctionContext cont)
        {
            context = cont;
        }

        public Boolean createBid(Entities.Item item, Entities.User by, Models.ViewModel.NewBidViewModel viewModel)
        {
            if (!this.validateViewModel(viewModel))
            {
                return false;
            }

            //bigger than topbid
            if (item.HasBid && item.TopBid.Price >= viewModel.Price)
            {
                return false;
            }

            //first bid
            if (!item.HasBid && item.OriginalBid > /*!=*/ viewModel.Price)
            {
                return false;
            }


            context.Bids.Add(new Entities.Bid
            {
                Price = viewModel.Price,
                Item = item,
                ItemId = item.Id,
                User = by,
                UserId = by.Id,
                CreatedAt = DateTime.Now
            });

            return save();
        }
    }
}

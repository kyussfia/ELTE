using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models.Entities;

namespace AuctionSite.Models.Db
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }
        public DbSet<Bid> Bids { get; set; }

        public bool isInitialized()
        {
            return
               Users.Any() &&
               Categories.Any() &&
               Items.Any() &&
               Advertisers.Any()
            ;
        }
    }
}

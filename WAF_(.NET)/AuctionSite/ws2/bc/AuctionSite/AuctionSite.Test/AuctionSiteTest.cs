using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models.Db;
using AuctionSite.Models.Entities;
using AuctionSite.Controllers;
using AuctionSite.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Query.Internal;

using System.Diagnostics;
using System.Threading.Tasks;

namespace AuctionSite.Test
{
    public class AuctionSiteTest :IDisposable
    {
        private readonly AuctionContext _context;
        private readonly List<CategoryDTO> _categoryDTOs;
        private readonly List<ItemDTO> _itemDTOs;
        private readonly List<AdvertiserDTO> _advertDTOs;
        private readonly List<UserDTO> _userDTOs;
        private readonly List<BidDTO> _bidDTOs;

        public AuctionSiteTest()
        {
            var options = new DbContextOptionsBuilder<AuctionContext>()
                .UseInMemoryDatabase("AuctionSiteTest")
                .Options;

            _context = new AuctionContext(options);
            _context.Database.EnsureCreated();

            // adatok inicializációja
            var categoryData = new List<Category>
            {
                new Category {Id = 1, Name = "TESTCATEGORY" }
            };
            _context.Categories.AddRange(categoryData);

            var advertiserData = new List<Advertiser>
            {
                new Advertiser { Id = 1, Name = "Mikus Márk", Password = new byte[] { }, Username = "kyussfia" }
            };
            _context.Advertisers.AddRange(advertiserData);


            var userData = new List<User>
            {
                new User { Id = 1, Name = "Teszt Elek", Password = new byte[] { }, Username = "tesztelek", PhoneNumber = "+1233445", Email = "test@email.hu" }
            };
            _context.Users.AddRange(userData);

            var itemData = new List<Item>
            {
                new Item {
                    Name = "TESTITEM1",
                    CategoryId = categoryData[0].Id,
                    AdvertiserId = advertiserData[0].Id,
                    OriginalBid = 500,
                    Description = "Leírás 1",
                    CreatedAt = DateTime.Now,
                    Currency = "Ft",
                    ClosedAt = DateTime.ParseExact("2108-12-01 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                },
            };
            _context.Items.AddRange(itemData);

            var bidData = new List<Bid>
            {
                new Bid { Id = 1, ItemId = itemData[0].Id, UserId = userData[0].Id, CreatedAt = DateTime.Now, Price = 5000 }
            };
            _context.Bids.AddRange(bidData);

            _context.SaveChanges();

            _categoryDTOs = categoryData.Select(cat => new CategoryDTO
            {
                Id = cat.Id,
                Name = cat.Name
            }).ToList();

            _advertDTOs = advertiserData.Select(a => new AdvertiserDTO
            {
                Id = a.Id,
                Name = a.Name,
                Username = a.Username
            }).ToList();

            _userDTOs = userData.Select(a => new UserDTO
            {
                Id = a.Id,
                Name = a.Name,
                Username = a.Username,
                PhoneNumber = a.PhoneNumber,
                Email = a.Email
            }).ToList();

            _itemDTOs = itemData.Select(item => new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Category = _categoryDTOs.Single(c => c.Id == item.CategoryId),
                OriginalBid = item.OriginalBid,
                Picture = item.Picture,
                Description = item.Description,
                CreatedAt = item.CreatedAt,
                Currency = item.Currency,
                CategoryId = item.CategoryId,
                AdvertiserId = item.AdvertiserId,                
                ClosedAt = item.ClosedAt
            }).ToList();

            _bidDTOs = bidData.Select(b => new BidDTO
            {
                Id = b.Id,
                ItemId = b.ItemId,
                UserId = b.UserId,
                User = _userDTOs.FirstOrDefault(u => u.Id == b.UserId),
                Item = _itemDTOs.FirstOrDefault(i => i.Id == b.ItemId),
                CreatedAt = b.CreatedAt,
                Price = b.Price
            }).ToList();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetCategories()
        {
            var controller = new CategoriesController(_context);
            var result = controller.GetCategories();

            // Assert
            Debug.WriteLine(result.ToString());
            var objectResult = Assert.IsType<List<CategoryDTO>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(objectResult);
            Assert.Equal(_categoryDTOs, model);
        }

        [Fact]
        public void GetItemsOfAdvertiser()
        {
            var controller = new ItemsController(_context);
            var advertiser = _context.Advertisers.FirstOrDefault();
            var result = controller.GetItems(advertiser.Id);

            // Assert
            var objectResult = Assert.IsType<List<ItemDTO>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ItemDTO>>(objectResult);
            Assert.Equal(_itemDTOs, model);
        }

        [Fact]
        public void PostItem()
        {
            var newItem = new ItemDTO
            {
                CategoryId = 1,
                Name = "TESTNEW",
                AdvertiserId = 1,
                ClosedAt = DateTime.ParseExact("2108-12-12 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                OriginalBid = 1000,
                Description = "HEy",
                Currency = "Ft",
                Picture = new Byte[] { },
                Category = _categoryDTOs.SingleOrDefault(c => c.Id == 1)
            };

            var controller = new ItemsController(_context);
            var result = controller.PostItem(newItem);

            // Assert
            var objectResult = Assert.IsType<CreatedAtRouteResult>(result);
            var model = Assert.IsAssignableFrom<ItemDTO>(objectResult.Value);
            Assert.Equal(_itemDTOs.Count + 1, _context.Items.Count());
            Assert.Equal(newItem, model);
        }

        [Fact]
        public void PutItem()
        {
            var Item = _itemDTOs.First(i => i.Id == 1);

            Item.ClosedAt = DateTime.Now;

            var controller = new ItemsController(_context);
            var result = controller.PutItem(Item);

            // Assert
            var objectResult = Assert.IsType<CreatedAtRouteResult>(result);
            var model = Assert.IsAssignableFrom<ItemDTO>(objectResult.Value);
            Assert.Equal(_itemDTOs.Count, _context.Items.Count());
            Assert.Equal(Item, model);

            Assert.Equal<DateTime>(Item.ClosedAt, model.ClosedAt);
        }

        [Fact]
        public void GetAdvertiserByUsername()
        {
            var controller = new AdvertisersController(_context);
            var result = controller.GetAdvertiser("kyussfia");
            var advertiser = _advertDTOs.SingleOrDefault(a => a.Username == "kyussfia");

            Assert.NotNull(advertiser);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Advertiser>(objectResult.Value);
            Assert.Equal(advertiser.Id, model.Id);
        }

        [Fact]
        public void GetBids()
        {
            var controller = new BidsController(_context);
            var result = controller.GetBids();

            // Assert
            var objectResult = Assert.IsType<List<BidDTO>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BidDTO>>(objectResult);
            Assert.Equal(_bidDTOs, model);
        }

        [Fact]
        public void GetBidsOfItem()
        {
            var item = _itemDTOs.FirstOrDefault();
            var controller = new BidsController(_context);
            var result = controller.GetBids(item.Id);

            Assert.NotNull(item);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Bid>>(objectResult.Value);
            Assert.Equal(_context.Bids.ToList(), model);
        }

        [Fact]
        public void GetItem()
        {
            var item = _itemDTOs.FirstOrDefault();
            var controller = new ItemsController(_context);
            var result = controller.GetItem(item.Id);

            Assert.NotNull(item);

            // Assert
            var taskResult = Assert.IsType<Task<IActionResult>>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(taskResult.Result);
            var model = Assert.IsAssignableFrom<Item>(objectResult.Value);
            Assert.Equal(item.Id, model.Id);
            Assert.Equal(item.CreatedAt, model.CreatedAt);
            Assert.Equal(item.Name, model.Name);
            Assert.Equal(item.CategoryId, model.CategoryId);
        }

        [Fact]
        public void GetCategory()
        {
            var cat = _categoryDTOs.FirstOrDefault();
            var controller = new CategoriesController(_context);
            var result = controller.GetCategory(cat.Id);

            Assert.NotNull(cat);

            // Assert
            var taskResult = Assert.IsType<Task<IActionResult>>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(taskResult.Result);
            var model = Assert.IsAssignableFrom<CategoryDTO>(objectResult.Value);
            Assert.Equal(cat.Id, model.Id);
            Assert.Equal(cat.Name, model.Name);
        }

        [Fact]
        public void GetUser()
        {
            var user = _userDTOs.FirstOrDefault();
            var controller = new UsersController(_context);
            var result = controller.GetUser(user.Id);

            Assert.NotNull(user);

            // Assert
            var taskResult = Assert.IsType<Task<IActionResult>>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(taskResult.Result);
            var model = Assert.IsAssignableFrom<UserDTO>(objectResult.Value);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(user.Name, model.Name);
            Assert.Equal(user.Email, model.Email);
            Assert.Equal(user.PhoneNumber, model.PhoneNumber);
            Assert.Equal(user.Username, model.Username);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AuctionSite.Models.Entities;
using System.Globalization;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Models.Db
{
    public class Initializer
    {
        private static AuctionContext context;

        private static IConfiguration config;

        private static void seedUsers()
        {
            var baseUsers = new User[]
            {
                new User{ Name = "Mikus Márk", Email = "kyussfia@gmail.com", Password = new byte[]{ }, PhoneNumber = "", Username = "kyussfia" }
            };
            foreach (User u in baseUsers)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        private static void seedCategories()
        {
            var cats = new Category[]
            {
                new Category { Name = "Fruits"},
                new Category { Name = "Tools"},
                new Category { Name = "Entertainment"},
                new Category { Name = "Art & Craft"},
                new Category { Name = "Clothing"},
                new Category { Name = "Miscellaneous"},
                new Category { Name = "Sports"}
            };
            foreach (Category u in cats)
            {
                context.Categories.Add(u);
            }
            context.SaveChanges();
        }

        private static void seedItems()
        {
            var img = LoadSampleImage().Result;
            var items = new Item[]
            {
                new Item
                {
                    Name = "Alma",
                    Description = "Gyümölcs",
                    ClosedAt = DateTime.ParseExact("2018-02-01 00:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    OriginalBid = 50,
                    CategoryId = 1,
                    Currency = "FT",
                    AdvertiserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-16 21:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    Picture = img

                },
                new Item
                {
                    Name = "Barack",
                    Description = "Gyümölcs",
                    ClosedAt = DateTime.ParseExact("2018-12-01 00:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    OriginalBid = 100,
                    Currency = "FT",
                    CategoryId = 1,
                    AdvertiserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-16 21:02:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    Picture = img

                },
                new Item
                {
                    Name = "Kalapács",
                    Description = "Szerszám",
                    ClosedAt = DateTime.ParseExact("2018-12-01 00:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    OriginalBid = 500,
                    Currency = "FT",
                    CategoryId = 2,
                    AdvertiserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-17 20:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    Picture = img
                },
                new Item
                {
                    Name = "Láncfűrész",
                    Description = "Szerszám",
                    ClosedAt = DateTime.ParseExact("2018-04-30 00:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    OriginalBid = 10000,
                    Currency = "FT",
                    CategoryId = 2,
                    AdvertiserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-10 11:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),

                },
                new Item
                {
                    Name = "Labda",
                    Description = "Játékszer",
                    ClosedAt = DateTime.ParseExact("2018-04-30 00:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    OriginalBid = 250,
                    Currency = "FT",
                    CategoryId = 3,
                    AdvertiserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-16 21:02:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                }
            };
            foreach (Item u in items)
            {
                context.Items.Add(u);
            }
            for (int i = 0; i < 22; i++)
            {
                Item t = new Item
                {
                    Name = "Test Tárgy" + i,
                    Description = "Teszt",
                    ClosedAt = DateTime.ParseExact("2018-07-01 00:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    OriginalBid = 50,
                    CategoryId = 6,
                    Currency = "FT",
                    AdvertiserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-16 21:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),

                };
                context.Items.Add(t);
            }
            context.SaveChanges();
        }

        private static void seedAdvertisers()
        {
            var baseAdv = new Advertiser[]
            {
                new Advertiser { Name = "Mikus Márk", Password = new byte[]{ }, Username = "kyussfia" }
            };
            foreach (Advertiser u in baseAdv)
            {
                context.Advertisers.Add(u);
            }
            context.SaveChanges();
        }

        private static void seedBids()
        {
            var bids = new Bid[]
            {
                new Bid
                {
                    ItemId = 3,
                    Price = 505,
                    UserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-18 21:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                },
                new Bid
                {
                    ItemId = 4,
                    Price = 10000,
                    UserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-18 21:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                },
                new Bid
                {
                    ItemId = 4,
                    Price = 11000,
                    UserId = 1,
                    CreatedAt = DateTime.ParseExact("2018-03-18 22:00:00",
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                }
            };
            foreach (Bid u in bids)
            {
                context.Bids.Add(u);
            }
            context.SaveChanges();
        }

        private static void migrateFirst()
        {
            seedUsers();
            seedCategories();
            seedAdvertisers();
            seedItems();
            seedBids();
        }

        public static void Initialize(IApplicationBuilder app, IConfiguration conf)
        {
            context = app.ApplicationServices
                .GetRequiredService<AuctionContext>();

            config = conf;
            initDb();
        }

        private static string getStaticDataDir()
        {
            return config.GetValue<string>("DataStore");
        }

        private static bool isStaticDataDirExist()
        {
            return Directory.Exists(getStaticDataDir());
        }

        private static void initDb()
        {
            // Version 1.0
            // Create first migration static
            //
            // Adatbázis létrehozása, amennyiben nem létezik
            //context.Database.EnsureCreated();

            // Version 2.0
            //
            // Adatbázis migrációk végrehajtása, amennyiben szükséges
            context.Database.Migrate();

            if (!context.isInitialized())
            {
                migrateFirst();
            }
        }

        private static async Task<byte[]> LoadSampleImage()
        {
            var file = new byte[] { };
            // Ellenőrizzük, hogy képek könyvtára létezik-e.
            var imageDirectory = config.GetValue<string>("DataStore");
            if (Directory.Exists(imageDirectory))
            {

                // Képek aszinkron betöltése.
                //var largePath = Path.Combine(imageDirectory, "petra_1.png");
                var path = Path.Combine(imageDirectory, "termek.png");
                if (File.Exists(path))
                {
                    file = await File.ReadAllBytesAsync(path);
                }
            }
            return file;
        }
    }
}

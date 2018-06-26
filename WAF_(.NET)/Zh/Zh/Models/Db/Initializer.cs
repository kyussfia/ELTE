using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Zh.Models.Db
{
    public class Initializer
    {
        private static ErrorContext context;

        private static IConfiguration config;

        public static void Initialize(IApplicationBuilder app, IConfiguration conf)
        {
            context = app.ApplicationServices
                .GetRequiredService<ErrorContext>();

            config = conf;
            initDb();
        }

        private static void initDb()
        {
            // Version 1.0
            // Create first migration static
            //
            // Adatbázis létrehozása, amennyiben nem létezik
            context.Database.EnsureCreated();

            // Version 2.0
            //
            // Adatbázis migrációk végrehajtása, amennyiben szükséges
           // context.Database.Migrate();

            if (!context.isInitialized())
            {
                migrateFirst();
            }
        }

        private static void seedUsers()
        {
            var baseUsers = new User[]
            {
                new User{ Email = "kyussfia@gmail.com", Password = new byte[]{ }, Username = "kyussfia" }
            };
            foreach (User u in baseUsers)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        private static void seedErrors()
        {
            var baseError = new Error[]
            {
                new Error{
                    CreatedAt = DateTime.Now,
                    Description = "Leírás",
                    NumOfRequests = 1,
                    Title = "Vizsga"
                }
            };
            foreach (Error u in baseError)
            {
                context.Errors.Add(u);
            }
            context.SaveChanges();
        }

        private static void seedRequest()
        {
            var baseUsers = new Request[]
            {
                new Request{ UserId = 1, ErrorId = 1 }
            };
            foreach (Request u in baseUsers)
            {
                context.Requests.Add(u);
            }
            context.SaveChanges();
        }

        private static void migrateFirst()
        {
            seedUsers();
            seedErrors();
            seedRequest();
        }
    }
}

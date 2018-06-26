using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System;

namespace WebApplication.Models
{
    public static class Db
    {
        public static void Initialize(IApplicationBuilder app)
        {
            ApplicationContext context = app.ApplicationServices
                .GetRequiredService<ApplicationContext>();

            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var items = new User[]
            {
                new User{ IsTakeCare = true, Name = "ELTE", Password = new byte[]{ }, Email = "asc@hu.ji" },
                new User{ IsTakeCare = false, Name = "ELTE1", Password = new byte[]{ },  Email = "asc@hu.hu" },
            };

            foreach (User i in items)
            {
                context.Users.Add(i);
            }

            var anims = new Animal[]
            {
                new Animal{ Name="Kutya1", CreatedAt = DateTime.ParseExact("2018-03-18 22:00:00","yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), isTook = false, Spec="Kutya" },
                new Animal{ Name="Macska1", CreatedAt = DateTime.ParseExact("2018-02-18 22:00:00","yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), isTook = false, Spec="Macska" }
            };

            foreach (Animal i in anims)
            {
                context.Animals.Add(i);
            }

            context.SaveChanges();
        }
    }
}

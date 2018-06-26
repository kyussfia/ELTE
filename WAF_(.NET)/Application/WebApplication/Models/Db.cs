using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Models
{
    public static class Db
    {
        public static void Initialize(IApplicationBuilder app)
        {
            //TODO  
            ApplicationContext context = app.ApplicationServices
                .GetRequiredService<ApplicationContext>();

            context.Database.EnsureCreated();

            /*if (context.Items.Any())
            {
                return;
            }

            var items = new Item[]
            {
                new Item{Name = "Item1" },
                new Item{Name = "Item2" },
                new Item{Name = "Item3" }
            };

            foreach (Item i in items)
            {
                context.Items.Add(i);
            }
            */
            context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
        }
        //TODO
        //public DbSet<Obj> Items { get; set; }
    }
}

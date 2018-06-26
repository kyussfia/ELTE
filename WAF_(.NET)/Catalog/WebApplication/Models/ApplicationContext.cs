using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Models
{
    public class ApplicationContext : DbContext
    {//0x4AEB2000B9DE5858F5E5E0B7EDA52F253CAF19582C67CBBB453BE6987ECC1BAF27D75670E39F78058FB1EBEE3D16B83D1CBDC8D3628636377B2458EA5BF12FF2
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}

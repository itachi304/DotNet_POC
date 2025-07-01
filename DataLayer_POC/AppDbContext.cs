using DataLayer_POC.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer_POC
{
    public class AppDbContext : DbContext
    {


        
         public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Product> Products => Set<Product>();
    }
}

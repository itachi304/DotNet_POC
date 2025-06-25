using DataLayer_POC.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer_POC
{
    public class AppDbContext : DbContext
    {


        public DbSet<Product> Products => Set<Product>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer("Server=PREDACER;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}

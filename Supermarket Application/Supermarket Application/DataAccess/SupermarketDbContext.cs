using Supermarket_Application.Models;
using System.Data.Entity;

namespace Supermarket_Application.DataAccess
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext() : base("name=SupermarketDbContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}

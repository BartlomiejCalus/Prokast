using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class ProkastServerDbContext: DbContext
    {
        public ProkastServerDbContext(DbContextOptions<ProkastServerDbContext> options) : base(options) { }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CustomParams> CustomParams { get; set; }
        public DbSet<DictionaryParams> DictionaryParams { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<AdditionalName> AdditionalNames { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdditionalDescription> AdditionalDescriptions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<StoredProduct> StoredProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderStatus)
                .HasConversion<string>();
            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentStatus)
                .HasConversion<string>();
        }
    }
}

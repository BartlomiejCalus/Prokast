using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.OrderID).IsRequired();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired().HasPrecision(10,2);
            builder.Property(x => x.TotalWeightKg).IsRequired().HasPrecision(10,2);
            builder.Property(x => x.PaymentMethod).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.TrackingID);
            builder.Property(x => x.IsBusiness).IsRequired();
            builder.Property(x => x.BusinessID);
            /*builder.HasMany(x => x.Products)
            .WithMany(x => x.Orders)
            .UsingEntity<Dictionary<string, object>>(
                "OrderProduct",
                l => l.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey("ProductsID")
                      .OnDelete(DeleteBehavior.Restrict), // lub NoAction
                r => r.HasOne<Order>()
                      .WithMany()
                      .HasForeignKey("OrdersID")
                      .OnDelete(DeleteBehavior.Cascade));*/
            //builder.HasOne(x => x.Client).WithMany(y => y.Orders).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(x => x.Buyer).WithMany(y => y.Orders).HasForeignKey(z => z.BuyerID).OnDelete(DeleteBehavior.Cascade);
            /*builder.HasMany(x => x.Products)
            .WithMany(y => y.Orders)
            .UsingEntity<Dictionary<string, object>>(
                "OrderProduct",
                j => j.HasOne<Product>()
                      .WithMany()
                      .IsRequired(false)
                      .HasForeignKey("ProductID")
                      .OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Order>()
                      .WithMany()
                      .IsRequired(false)
                      .HasForeignKey("OrderID")
                      .OnDelete(DeleteBehavior.Restrict));*/
        }
    }
}

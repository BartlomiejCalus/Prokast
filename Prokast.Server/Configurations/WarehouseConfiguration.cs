using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class WarehouseConfiguration: IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(6);
            builder.Property(x => x.City).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Country).IsRequired().HasMaxLength(30);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(11);
            //builder.HasOne(x => x.Client).WithMany(y => y.Warehouses).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.StoredProducts).WithOne(y => y.Warehouse).HasForeignKey(z => z.WarehouseID).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(x => x.Accounts).WithOne(y => y.Warehouse).HasForeignKey(z => z.WarehouseID);
        }
    }
}

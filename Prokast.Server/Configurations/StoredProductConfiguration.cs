using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class StoredProductConfiguration: IEntityTypeConfiguration<StoredProduct>
    {
        public void Configure(EntityTypeBuilder<StoredProduct> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Quantity).IsRequired().HasMaxLength(5);
            builder.Property(x => x.MinQuantity).IsRequired().HasMaxLength(5);
            builder.Property(x => x.LastUpdated).IsRequired();
            builder.HasOne(x => x.Product).WithOne(y => y.StoredProduct).HasForeignKey<StoredProduct>(z => z.ProductID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class PriceListsConfiguration: IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.Prices).WithOne(y => y.PriceLists).HasForeignKey(z => z.PriceListID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Product).WithOne(y => y.PriceList).HasForeignKey<PriceList>(z => z.ProductID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

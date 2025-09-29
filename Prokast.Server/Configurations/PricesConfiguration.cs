using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class PricesConfiguration: IEntityTypeConfiguration<Prices>
    {
        public void Configure(EntityTypeBuilder<Prices> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Netto).IsRequired().HasPrecision(10,2);
            builder.Property(x => x.VAT).IsRequired().HasPrecision(10,2);
            builder.Property(x => x.Brutto).IsRequired().HasPrecision(10, 2);
            builder.HasOne(x => x.Regions).WithMany().HasForeignKey(y => y.RegionID);
            //builder.HasOne(x => x.PriceLists).WithMany(y => y.Prices).HasForeignKey(z => z.PriceListID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

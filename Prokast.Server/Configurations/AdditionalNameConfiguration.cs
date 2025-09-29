using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class AdditionalNameConfiguration: IEntityTypeConfiguration<AdditionalName>
    {
        public void Configure(EntityTypeBuilder<AdditionalName> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(200);
            //builder.HasOne(x => x.Product).WithMany(y => y.AdditionalNames).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Regions).WithMany().HasForeignKey(y => y.RegionID);
        }
    }
}

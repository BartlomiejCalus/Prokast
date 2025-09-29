using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class AdditionalDescriptionConfiguration : IEntityTypeConfiguration<AdditionalDescription>
    {
        public void Configure(EntityTypeBuilder<AdditionalDescription> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(1000);
            //builder.HasOne(x => x.Product).WithMany(y => y.AdditionalDescriptions).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Regions).WithMany().HasForeignKey(y => y.RegionID);
        }
    }
}

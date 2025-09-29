using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class CustomParamsConfiguration: IEntityTypeConfiguration<CustomParams>
    {
        public void Configure(EntityTypeBuilder<CustomParams> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            //builder.HasOne(x => x.Product).WithMany(y => y.CustomParams).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Regions).WithMany().HasForeignKey(y => y.RegionID);
        }
    }
}

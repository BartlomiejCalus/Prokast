using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class DictionaryParamConfiguration: IEntityTypeConfiguration<DictionaryParams>
    {
        public void Configure(EntityTypeBuilder<DictionaryParams> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Type).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(100);
            builder.Property(x => x.OptionID).IsRequired().HasMaxLength(4);
            builder.HasOne(x => x.Regions).WithMany().HasForeignKey(x => x.RegionID);
        }
    }
}

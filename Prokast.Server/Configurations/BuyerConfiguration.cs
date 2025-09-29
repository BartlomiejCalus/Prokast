using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class BuyerConfiguration: IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.HouseNumber).HasMaxLength(3);
            builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(6);
            builder.Property(x => x.City).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Country).IsRequired().HasMaxLength(30);
            builder.Property(x => x.NIP).IsRequired().HasMaxLength(15);
            builder.HasMany(x => x.Orders).WithOne(y => y.Buyer).HasForeignKey(x => x.BuyerID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class ClientConfiguration:IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BusinessName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NIP).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(6);
            builder.Property(x => x.City).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Country).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Subscription).IsRequired();
            builder.HasMany(x => x.Orders).WithOne(y => y.Client).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Products).WithOne(y => y.Client).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Accounts).WithOne(y => y.Client).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Warehouses).WithOne(y => y.Client).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

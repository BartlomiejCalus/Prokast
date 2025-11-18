using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class AccountConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleID);
            //builder.Property(x => x.Role).HasMaxLength(5);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            //builder.HasOne(x => x.Client).WithMany(y => y.Accounts).HasForeignKey(z => z.ClientID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Warehouse).WithMany(y => y.Accounts).HasForeignKey(z => z.WarehouseID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

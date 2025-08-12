﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokast.Server.Entities;

namespace Prokast.Server.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.SKU).IsRequired().HasMaxLength(100);
            builder.Property(x => x.EAN).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.AdditionDate).IsRequired();
            builder.Property(x => x.ModificationDate).IsRequired();
            builder.HasMany(x => x.AdditionalDescriptions).WithOne(y => y.Product).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.AdditionalNames).WithOne(y => y.Product).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.CustomParams).WithOne(y => y.Product).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.DictionaryParams).WithMany();
/*            builder.HasMany(p => p.DictionaryParams)
                           .WithMany(dp => dp.Products)
                           .UsingEntity<Dictionary<string, object>>(
                                "ProductDictionaryParam", // nazwa tabeli łączącej
                                right => right.HasOne<DictionaryParams>()
                                              .WithMany()
                                              .HasForeignKey("DictionaryParamID")
                                              .OnDelete(DeleteBehavior.Cascade),
                                left => left.HasOne<Product>()
                                            .WithMany()
                                            .HasForeignKey("ProductID")
                                            .OnDelete(DeleteBehavior.Cascade)
                );*/ 
            builder.HasMany(x => x.Photos).WithOne(y => y.Product).HasForeignKey(z => z.ProductID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.StoredProduct).WithOne(y => y.Product).HasForeignKey<Product>(z => z.StoredProductID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

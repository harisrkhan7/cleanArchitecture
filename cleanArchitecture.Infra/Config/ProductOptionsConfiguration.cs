﻿using System;
using cleanArchitecture.Core.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cleanArchitecture.Infra.Data.Config
{
    public class ProductOptionsConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public ProductOptionsConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.ToTable("ProductOption");

            builder.HasKey(po => po.Id);

            builder.Property(po => po.Id)                
                .IsRequired();

            builder.Property(po => po.Name)
                .IsRequired();

            builder.Property(po => po.Description)
                .IsRequired();

            builder.Property(po => po.ProductId);

            builder.HasOne<Product>(po => po.Product)
                .WithMany(pr => pr.ProductOptions)
                .HasForeignKey(po => po.ProductId);              
        }
    }
}

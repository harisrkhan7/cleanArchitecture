using cleanArchitecture.Core.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cleanArchitecture.Infra.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.Id)                
                .IsRequired();

            builder.Property(pr => pr.Name)
                .IsRequired();

            builder.Property(pr => pr.Description)
                .IsRequired();

            builder.Property(pr => pr.Price)
                .IsRequired();

            builder.Property(pr => pr.DeliveryPrice)
                .IsRequired();

            builder.Property(pr => pr.IsNew)
                .IsRequired();
        }
        
    }
}

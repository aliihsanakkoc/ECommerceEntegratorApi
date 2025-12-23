using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.ProductCode).HasColumnName("ProductCode").IsRequired().HasMaxLength(15);
        builder.Property(p => p.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(50);
        builder.Property(p => p.ProductDescription).HasColumnName("ProductDescription").HasMaxLength(100);
        builder.Property(p => p.ProductImageUrl).HasColumnName("ProductImageUrl").HasMaxLength(150);
        builder.Property(p => p.ProductPrice).HasColumnName("ProductPrice").IsRequired().HasColumnType("money");
        builder.Property(p => p.IsAddToCart).HasColumnName("IsAddToCart").IsRequired();
        builder.Property(p => p.ProductType).HasColumnName("ProductType").IsRequired().HasMaxLength(20);
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}

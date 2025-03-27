using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
{
    public void Configure(EntityTypeBuilder<CategoryProduct> builder)
    {
        builder.ToTable("CategoryProducts").HasKey(cp => cp.Id);

        builder.Property(cp => cp.Id).HasColumnName("Id").IsRequired();
        builder.Property(cp => cp.CategoryId).HasColumnName("CategoryId").IsRequired();
        builder.Property(cp => cp.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(cp => cp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cp => cp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cp => cp.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cp => !cp.DeletedDate.HasValue);

        builder.HasOne(cp => cp.Category);
        builder.HasOne(cp => cp.Product);
        builder.HasIndex(cp => new {cp.ProductId, cp.CategoryId}).IsUnique();
    }
}
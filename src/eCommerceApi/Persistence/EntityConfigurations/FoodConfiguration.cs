using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FoodConfiguration : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.ToTable("Foods").HasKey(f => f.Id);

        builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
        builder.Property(f => f.StorageCondition).HasColumnName("StorageCondition").IsRequired().HasMaxLength(50);
        builder.Property(f => f.PreparationTechnique).HasColumnName("PreparationTechnique").HasMaxLength(200);
        builder.Property(f => f.ExpiryDate).HasColumnName("ExpiryDate").IsRequired();
        builder.Property(f => f.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(f => !f.DeletedDate.HasValue);

        builder.HasIndex(f => f.ProductId).IsUnique();
    }
}
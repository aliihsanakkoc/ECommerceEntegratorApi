using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClothingConfiguration : IEntityTypeConfiguration<Clothing>
{
    public void Configure(EntityTypeBuilder<Clothing> builder)
    {
        builder.ToTable("Clothings").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.MadeIn).HasColumnName("MadeIn").IsRequired().HasMaxLength(20);
        builder.Property(c => c.FiberComposition).HasColumnName("FiberComposition").IsRequired().HasMaxLength(100);
        builder.Property(c => c.LaundryLabel).HasColumnName("LaundryLabel").HasMaxLength(200);
        builder.Property(c => c.Brand).HasColumnName("Brand").HasMaxLength(20);
        builder.Property(c => c.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);

        builder.HasIndex(c => c.ProductId).IsUnique();
    }
}
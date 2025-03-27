using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class VariantProductConfiguration : IEntityTypeConfiguration<VariantProduct>
{
    public void Configure(EntityTypeBuilder<VariantProduct> builder)
    {
        builder.ToTable("VariantProducts").HasKey(vp => vp.Id);

        builder.Property(vp => vp.Id).HasColumnName("Id").IsRequired();
        builder.Property(vp => vp.VariantId).HasColumnName("VariantId").IsRequired();
        builder.Property(vp => vp.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(vp => vp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(vp => vp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(vp => vp.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(vp => !vp.DeletedDate.HasValue);

        builder.HasIndex(vp => new { vp.VariantId, vp.ProductId }).IsUnique();
    }
}
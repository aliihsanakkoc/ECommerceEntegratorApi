using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class VariantConfiguration : IEntityTypeConfiguration<Variant>
{
    public void Configure(EntityTypeBuilder<Variant> builder)
    {
        builder.ToTable("Variants").HasKey(v => v.Id);

        builder.Property(v => v.Id).HasColumnName("Id").IsRequired();
        builder.Property(v => v.VariantName).HasColumnName("VariantName").IsRequired().HasMaxLength(20);
        builder.Property(v => v.TopVariantName).HasColumnName("TopVariantName").IsRequired().HasMaxLength(20);
        builder.Property(v => v.TopVariantId).HasColumnName("TopVariantId").IsRequired();
        builder.Property(v => v.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(v => v.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(v => v.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(v => !v.DeletedDate.HasValue);

        builder.HasOne(v => v.TopVariant)
            .WithMany(v => v.SubVariants)
            .HasForeignKey(v => v.TopVariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(v => v.VariantName).IsUnique();    

        builder.HasData(
            new Variant
            {
                Id = 1,
                VariantName = "Variant",
                TopVariantName = "Seed",
                TopVariantId = 1,
            }
        );
    }
}
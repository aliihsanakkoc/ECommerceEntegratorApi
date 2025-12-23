using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.SingleCategoryName).HasColumnName("SingleCategoryName").IsRequired().HasMaxLength(30);
        builder.Property(c => c.FullCategoryName).HasColumnName("FullCategoryName").IsRequired().HasMaxLength(200);
        builder.Property(c => c.IsProductCategorization).HasColumnName("IsProductCategorization").IsRequired();
        builder.Property(c => c.TopCategoryName).HasColumnName("TopCategoryName").IsRequired().HasMaxLength(200);
        builder.Property(c => c.TopCategoryId).HasColumnName("TopCategoryId").IsRequired();
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);

        builder
            .HasOne(c => c.TopCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.TopCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Category("Top Category", "Seed Top Category")
            {
                Id = 1,
                TopCategoryId = 1,
                IsProductCategorization = false
            }
        );

        builder.HasIndex(c => c.FullCategoryName).IsUnique();
    }
}

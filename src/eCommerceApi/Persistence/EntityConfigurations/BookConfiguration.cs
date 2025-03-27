using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.Title).HasColumnName("Title").IsRequired().HasMaxLength(50);
        builder.Property(b => b.Author).HasColumnName("Author").IsRequired().HasMaxLength(50);
        builder.Property(b => b.ISBN).HasColumnName("ISBN").IsRequired().HasMaxLength(13);
        builder.Property(b => b.Publisher).HasColumnName("Publisher").IsRequired().HasMaxLength(50);
        builder.Property(b => b.PublishedDate).HasColumnName("PublishedDate").IsRequired();
        builder.Property(b => b.Edition).HasColumnName("Edition").IsRequired();
        builder.Property(b => b.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);

        builder.HasIndex(b => b.ProductId).IsUnique();
    }
}
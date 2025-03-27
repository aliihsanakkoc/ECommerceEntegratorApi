using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Product : Entity<int>
{
    public string ProductCode { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string? ProductDescription { get; set; }
    public string? ProductImageUrl { get; set; }
    public decimal ProductPrice { get; set; }   
    public bool IsAddToCart { get; set; } = true;
    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = [];
    public virtual ICollection<VariantProduct> VariantProducts { get; set; } = [];
    public string ProductType { get; set; } = default!; 
}
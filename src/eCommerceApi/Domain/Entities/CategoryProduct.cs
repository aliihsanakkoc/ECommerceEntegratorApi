using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class CategoryProduct : Entity<int>
{
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}
using Domain.Abstracts;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Food : Entity<int>, IProduct
{
    public string StorageCondition { get; set; } = default!;
    public string? PreparationTechnique { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}

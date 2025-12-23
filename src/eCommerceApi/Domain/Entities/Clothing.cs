using Domain.Abstracts;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Clothing : Entity<int>, IProduct
{
    public string MadeIn { get; set; } = default!;
    public string FiberComposition { get; set; } = default!;
    public string? LaundryLabel { get; set; }
    public string? Brand { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}

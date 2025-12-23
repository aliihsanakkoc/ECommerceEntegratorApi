using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class VariantProduct : Entity<int>
{
    public int VariantId { get; set; }
    public virtual Variant Variant { get; set; } = default!;
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}

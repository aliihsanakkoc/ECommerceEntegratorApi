using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Variant : Entity<int>
{
    public string VariantName { get; set; } = default!;
    public string TopVariantName { get; set; } = default!;
    public int TopVariantId { get; set; }
    public virtual Variant TopVariant { get; set; } = default!;
    public virtual ICollection<Variant> SubVariants { get; set; } = [];   
    public virtual ICollection<VariantProduct> VariantProducts { get; set; } = [];   

}

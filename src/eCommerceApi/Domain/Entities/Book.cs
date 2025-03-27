using Domain.Abstracts;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Book : Entity<int>, IProduct
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public string Publisher { get; set; } = default!;
    public DateTime PublishedDate { get; set; }
    public int Edition { get; set; }    
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
}

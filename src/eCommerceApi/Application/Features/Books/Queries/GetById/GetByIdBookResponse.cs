using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Queries.GetById;

public class GetByIdBookResponse : IResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public DateTime PublishedDate { get; set; }
    public int Edition { get; set; }
    public int ProductId { get; set; }
}

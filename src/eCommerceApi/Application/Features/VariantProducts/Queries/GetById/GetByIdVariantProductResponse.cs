using NArchitecture.Core.Application.Responses;

namespace Application.Features.VariantProducts.Queries.GetById;

public class GetByIdVariantProductResponse : IResponse
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int ProductId { get; set; }
}

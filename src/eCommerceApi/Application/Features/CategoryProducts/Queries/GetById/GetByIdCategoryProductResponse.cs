using NArchitecture.Core.Application.Responses;

namespace Application.Features.CategoryProducts.Queries.GetById;

public class GetByIdCategoryProductResponse : IResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
}

using NArchitecture.Core.Application.Responses;

namespace Application.Features.CategoryProducts.Commands.Update;

public class UpdatedCategoryProductResponse : IResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
}

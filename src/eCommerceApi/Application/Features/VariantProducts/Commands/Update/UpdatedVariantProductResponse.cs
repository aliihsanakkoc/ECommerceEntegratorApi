using NArchitecture.Core.Application.Responses;

namespace Application.Features.VariantProducts.Commands.Update;

public class UpdatedVariantProductResponse : IResponse
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int ProductId { get; set; }
}

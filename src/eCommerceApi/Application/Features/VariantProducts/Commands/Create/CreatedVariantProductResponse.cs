using NArchitecture.Core.Application.Responses;

namespace Application.Features.VariantProducts.Commands.Create;

public class CreatedVariantProductResponse : IResponse
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int ProductId { get; set; }
}
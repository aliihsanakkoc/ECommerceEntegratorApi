using NArchitecture.Core.Application.Responses;

namespace Application.Features.VariantProducts.Commands.Delete;

public class DeletedVariantProductResponse : IResponse
{
    public int Id { get; set; }
}

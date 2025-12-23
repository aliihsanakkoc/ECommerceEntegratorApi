using NArchitecture.Core.Application.Responses;

namespace Application.Features.Variants.Commands.Delete;

public class DeletedVariantResponse : IResponse
{
    public int Id { get; set; }
}

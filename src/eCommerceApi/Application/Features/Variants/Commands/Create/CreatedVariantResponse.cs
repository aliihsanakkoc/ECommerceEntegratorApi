using NArchitecture.Core.Application.Responses;

namespace Application.Features.Variants.Commands.Create;

public class CreatedVariantResponse : IResponse
{
    public int Id { get; set; }
    public string VariantName { get; set; }
    public string TopVariantName { get; set; }
    public int TopVariantId { get; set; }
}
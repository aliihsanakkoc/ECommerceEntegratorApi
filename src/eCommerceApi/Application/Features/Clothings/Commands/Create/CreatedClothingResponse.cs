using NArchitecture.Core.Application.Responses;

namespace Application.Features.Clothings.Commands.Create;

public class CreatedClothingResponse : IResponse
{
    public int Id { get; set; }
    public string MadeIn { get; set; }
    public string FiberComposition { get; set; }
    public string? LaundryLabel { get; set; }
    public string? Brand { get; set; }
    public int ProductId { get; set; }
}
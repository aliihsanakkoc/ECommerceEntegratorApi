using NArchitecture.Core.Application.Responses;

namespace Application.Features.Clothings.Commands.Delete;

public class DeletedClothingResponse : IResponse
{
    public int Id { get; set; }
}

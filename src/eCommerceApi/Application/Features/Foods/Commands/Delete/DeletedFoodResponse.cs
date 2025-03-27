using NArchitecture.Core.Application.Responses;

namespace Application.Features.Foods.Commands.Delete;

public class DeletedFoodResponse : IResponse
{
    public int Id { get; set; }
}
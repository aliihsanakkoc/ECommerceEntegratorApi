using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Commands.Delete;

public class DeletedBookResponse : IResponse
{
    public int Id { get; set; }
}
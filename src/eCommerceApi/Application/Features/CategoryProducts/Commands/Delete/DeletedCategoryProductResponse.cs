using NArchitecture.Core.Application.Responses;

namespace Application.Features.CategoryProducts.Commands.Delete;

public class DeletedCategoryProductResponse : IResponse
{
    public int Id { get; set; }
}
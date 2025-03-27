using NArchitecture.Core.Application.Dtos;

namespace Application.Features.CategoryProducts.Queries.GetList;

public class GetListCategoryProductListItemDto : IDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
}
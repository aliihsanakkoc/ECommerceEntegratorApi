using NArchitecture.Core.Application.Dtos;

namespace Application.Features.VariantProducts.Queries.GetList;

public class GetListVariantProductListItemDto : IDto
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int ProductId { get; set; }
}
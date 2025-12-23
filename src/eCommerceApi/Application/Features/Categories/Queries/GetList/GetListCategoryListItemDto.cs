using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryListItemDto : IDto
{
    public int Id { get; set; }
    public string SingleCategoryName { get; set; }
    public string? FullCategoryName { get; set; }
    public bool IsProductCategorization { get; set; }
    public string TopCategoryName { get; set; }
    public int TopCategoryId { get; set; }
}

using NArchitecture.Core.Application.Responses;

namespace Application.Features.Categories.Queries.GetById;

public class GetByIdCategoryResponse : IResponse
{
    public int Id { get; set; }
    public string SingleCategoryName { get; set; }
    public string? FullCategoryName { get; set; }
    public bool IsProductCategorization { get; set; }
    public string TopCategoryName { get; set; }
    public int TopCategoryId { get; set; }
}
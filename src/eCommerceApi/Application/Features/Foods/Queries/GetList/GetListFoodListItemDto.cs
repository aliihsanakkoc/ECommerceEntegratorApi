using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Foods.Queries.GetList;

public class GetListFoodListItemDto : IDto
{
    public int Id { get; set; }
    public string StorageCondition { get; set; }
    public string? PreparationTechnique { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int ProductId { get; set; }
}
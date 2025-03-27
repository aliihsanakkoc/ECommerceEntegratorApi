using NArchitecture.Core.Application.Responses;

namespace Application.Features.Foods.Queries.GetById;

public class GetByIdFoodResponse : IResponse
{
    public int Id { get; set; }
    public string StorageCondition { get; set; }
    public string? PreparationTechnique { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int ProductId { get; set; }
}
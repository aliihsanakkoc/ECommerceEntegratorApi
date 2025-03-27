using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Clothings.Queries.GetList;

public class GetListClothingListItemDto : IDto
{
    public int Id { get; set; }
    public string MadeIn { get; set; }
    public string FiberComposition { get; set; }
    public string? LaundryLabel { get; set; }
    public string? Brand { get; set; }
    public int ProductId { get; set; }
}
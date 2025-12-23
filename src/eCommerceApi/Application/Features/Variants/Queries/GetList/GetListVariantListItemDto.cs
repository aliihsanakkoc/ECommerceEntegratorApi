using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Variants.Queries.GetList;

public class GetListVariantListItemDto : IDto
{
    public int Id { get; set; }
    public string VariantName { get; set; }
    public string TopVariantName { get; set; }
    public int TopVariantId { get; set; }
}

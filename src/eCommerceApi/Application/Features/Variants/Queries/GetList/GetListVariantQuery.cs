using Application.Features.Variants.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Variants.Constants.VariantsOperationClaims;

namespace Application.Features.Variants.Queries.GetList;

public class GetListVariantQuery : IRequest<GetListResponse<GetListVariantListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListVariants({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetVariants";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListVariantQueryHandler : IRequestHandler<GetListVariantQuery, GetListResponse<GetListVariantListItemDto>>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;

        public GetListVariantQueryHandler(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListVariantListItemDto>> Handle(GetListVariantQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Variant> variants = await _variantRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListVariantListItemDto> response = _mapper.Map<GetListResponse<GetListVariantListItemDto>>(variants);
            return response;
        }
    }
}
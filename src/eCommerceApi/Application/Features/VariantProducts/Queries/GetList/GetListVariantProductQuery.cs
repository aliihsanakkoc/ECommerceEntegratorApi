using Application.Features.VariantProducts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;

namespace Application.Features.VariantProducts.Queries.GetList;

public class GetListVariantProductQuery : IRequest<GetListResponse<GetListVariantProductListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListVariantProducts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetVariantProducts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListVariantProductQueryHandler : IRequestHandler<GetListVariantProductQuery, GetListResponse<GetListVariantProductListItemDto>>
    {
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly IMapper _mapper;

        public GetListVariantProductQueryHandler(IVariantProductRepository variantProductRepository, IMapper mapper)
        {
            _variantProductRepository = variantProductRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListVariantProductListItemDto>> Handle(GetListVariantProductQuery request, CancellationToken cancellationToken)
        {
            IPaginate<VariantProduct> variantProducts = await _variantProductRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListVariantProductListItemDto> response = _mapper.Map<GetListResponse<GetListVariantProductListItemDto>>(variantProducts);
            return response;
        }
    }
}
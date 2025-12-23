using Application.Features.CategoryProducts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.CategoryProducts.Constants.CategoryProductsOperationClaims;

namespace Application.Features.CategoryProducts.Queries.GetList;

public class GetListCategoryProductQuery
    : IRequest<GetListResponse<GetListCategoryProductListItemDto>>,
        ISecuredRequest,
        ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCategoryProducts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCategoryProducts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCategoryProductQueryHandler
        : IRequestHandler<GetListCategoryProductQuery, GetListResponse<GetListCategoryProductListItemDto>>
    {
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IMapper _mapper;

        public GetListCategoryProductQueryHandler(ICategoryProductRepository categoryProductRepository, IMapper mapper)
        {
            _categoryProductRepository = categoryProductRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCategoryProductListItemDto>> Handle(
            GetListCategoryProductQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<CategoryProduct> categoryProducts = await _categoryProductRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCategoryProductListItemDto> response = _mapper.Map<
                GetListResponse<GetListCategoryProductListItemDto>
            >(categoryProducts);
            return response;
        }
    }
}

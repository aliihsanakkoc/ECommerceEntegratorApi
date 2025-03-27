using Application.Features.Foods.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Foods.Constants.FoodsOperationClaims;

namespace Application.Features.Foods.Queries.GetList;

public class GetListFoodQuery : IRequest<GetListResponse<GetListFoodListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListFoods({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetFoods";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFoodQueryHandler : IRequestHandler<GetListFoodQuery, GetListResponse<GetListFoodListItemDto>>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public GetListFoodQueryHandler(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFoodListItemDto>> Handle(GetListFoodQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Food> foods = await _foodRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFoodListItemDto> response = _mapper.Map<GetListResponse<GetListFoodListItemDto>>(foods);
            return response;
        }
    }
}
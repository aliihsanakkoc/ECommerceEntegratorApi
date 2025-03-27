using Application.Features.Clothings.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Clothings.Constants.ClothingsOperationClaims;

namespace Application.Features.Clothings.Queries.GetList;

public class GetListClothingQuery : IRequest<GetListResponse<GetListClothingListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListClothings({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetClothings";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClothingQueryHandler : IRequestHandler<GetListClothingQuery, GetListResponse<GetListClothingListItemDto>>
    {
        private readonly IClothingRepository _clothingRepository;
        private readonly IMapper _mapper;

        public GetListClothingQueryHandler(IClothingRepository clothingRepository, IMapper mapper)
        {
            _clothingRepository = clothingRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListClothingListItemDto>> Handle(GetListClothingQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Clothing> clothings = await _clothingRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClothingListItemDto> response = _mapper.Map<GetListResponse<GetListClothingListItemDto>>(clothings);
            return response;
        }
    }
}
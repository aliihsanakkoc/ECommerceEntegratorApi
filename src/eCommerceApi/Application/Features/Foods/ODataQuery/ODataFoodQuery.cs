using Application.Features.Foods.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Foods.Constants.FoodsOperationClaims;

namespace Application.Features.Foods.ODataQuery;

public class ODataFoodQuery : IRequest<IQueryable<GetListFoodListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin, Client];

    public class ODataFoodQueryHandler : IRequestHandler<ODataFoodQuery, IQueryable<GetListFoodListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;

        public ODataFoodQueryHandler(IMapper mapper, IFoodRepository foodRepository)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
        }

        public Task<IQueryable<GetListFoodListItemDto>> Handle(ODataFoodQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Food> foods = _foodRepository.Query();
            return Task.FromResult(foods.ProjectTo<GetListFoodListItemDto>(_mapper.ConfigurationProvider));
        }
    }
}

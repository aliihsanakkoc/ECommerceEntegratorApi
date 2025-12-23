using Application.Features.Foods.Constants;
using Application.Features.Foods.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Foods.Constants.FoodsOperationClaims;

namespace Application.Features.Foods.Queries.GetById;

public class GetByIdFoodQuery : IRequest<GetByIdFoodResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdFoodQueryHandler : IRequestHandler<GetByIdFoodQuery, GetByIdFoodResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly FoodBusinessRules _foodBusinessRules;

        public GetByIdFoodQueryHandler(IMapper mapper, IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _foodBusinessRules = foodBusinessRules;
        }

        public async Task<GetByIdFoodResponse> Handle(GetByIdFoodQuery request, CancellationToken cancellationToken)
        {
            Food? food = await _foodRepository.GetAsync(
                predicate: f => f.ProductId == request.Id,
                cancellationToken: cancellationToken
            );
            await _foodBusinessRules.FoodShouldExistWhenSelected(food);

            GetByIdFoodResponse response = _mapper.Map<GetByIdFoodResponse>(food);
            return response;
        }
    }
}

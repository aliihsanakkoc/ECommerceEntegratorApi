using Application.Features.Foods.Constants;
using Application.Features.Foods.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Foods.Constants.FoodsOperationClaims;

namespace Application.Features.Foods.Commands.Create;

public class CreateFoodCommand
    : IRequest<CreatedFoodResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public required string StorageCondition { get; set; }
    public string? PreparationTechnique { get; set; }
    public required DateTime ExpiryDate { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, FoodsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFoods"];

    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, CreatedFoodResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly FoodBusinessRules _foodBusinessRules;

        public CreateFoodCommandHandler(IMapper mapper, IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _foodBusinessRules = foodBusinessRules;
        }

        public async Task<CreatedFoodResponse> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            Food food = _mapper.Map<Food>(request);

            await _foodRepository.AddAsync(food);

            CreatedFoodResponse response = _mapper.Map<CreatedFoodResponse>(food);
            return response;
        }
    }
}

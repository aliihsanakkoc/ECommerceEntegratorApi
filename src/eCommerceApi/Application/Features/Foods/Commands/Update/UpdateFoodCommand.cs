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

namespace Application.Features.Foods.Commands.Update;

public class UpdateFoodCommand
    : IRequest<UpdatedFoodResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public required string StorageCondition { get; set; }
    public string? PreparationTechnique { get; set; }
    public required DateTime ExpiryDate { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, FoodsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFoods"];

    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, UpdatedFoodResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly FoodBusinessRules _foodBusinessRules;

        public UpdateFoodCommandHandler(IMapper mapper, IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _foodBusinessRules = foodBusinessRules;
        }

        public async Task<UpdatedFoodResponse> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            Food? food = await _foodRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _foodBusinessRules.FoodShouldExistWhenSelected(food);
            food = _mapper.Map(request, food);

            await _foodRepository.UpdateAsync(food!);

            UpdatedFoodResponse response = _mapper.Map<UpdatedFoodResponse>(food);
            return response;
        }
    }
}

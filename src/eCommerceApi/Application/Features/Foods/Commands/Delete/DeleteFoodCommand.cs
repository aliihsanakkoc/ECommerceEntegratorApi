using Application.Features.Foods.Constants;
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

namespace Application.Features.Foods.Commands.Delete;

public class DeleteFoodCommand
    : IRequest<DeletedFoodResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, FoodsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFoods"];

    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, DeletedFoodResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly FoodBusinessRules _foodBusinessRules;

        public DeleteFoodCommandHandler(IMapper mapper, IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _foodBusinessRules = foodBusinessRules;
        }

        public async Task<DeletedFoodResponse> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            Food? food = await _foodRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _foodBusinessRules.FoodShouldExistWhenSelected(food);

            await _foodRepository.DeleteAsync(food!);

            DeletedFoodResponse response = _mapper.Map<DeletedFoodResponse>(food);
            return response;
        }
    }
}

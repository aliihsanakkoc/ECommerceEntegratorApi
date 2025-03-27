using Application.Features.Foods.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Foods.Rules;

public class FoodBusinessRules : BaseBusinessRules
{
    private readonly IFoodRepository _foodRepository;
    private readonly ILocalizationService _localizationService;

    public FoodBusinessRules(IFoodRepository foodRepository, ILocalizationService localizationService)
    {
        _foodRepository = foodRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, FoodsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FoodShouldExistWhenSelected(Food? food)
    {
        if (food == null)
            await throwBusinessException(FoodsBusinessMessages.FoodNotExists);
    }

    public async Task FoodIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Food? food = await _foodRepository.GetAsync(
            predicate: f => f.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FoodShouldExistWhenSelected(food);
    }
}
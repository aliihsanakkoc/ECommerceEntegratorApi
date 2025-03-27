using Application.Features.Clothings.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Clothings.Rules;

public class ClothingBusinessRules : BaseBusinessRules
{
    private readonly IClothingRepository _clothingRepository;
    private readonly ILocalizationService _localizationService;

    public ClothingBusinessRules(IClothingRepository clothingRepository, ILocalizationService localizationService)
    {
        _clothingRepository = clothingRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ClothingsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ClothingShouldExistWhenSelected(Clothing? clothing)
    {
        if (clothing == null)
            await throwBusinessException(ClothingsBusinessMessages.ClothingNotExists);
    }

    public async Task ClothingIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Clothing? clothing = await _clothingRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ClothingShouldExistWhenSelected(clothing);
    }
}
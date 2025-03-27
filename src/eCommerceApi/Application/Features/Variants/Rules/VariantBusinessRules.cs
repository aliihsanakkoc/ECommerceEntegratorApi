using Application.Features.Variants.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Variants.Rules;

public class VariantBusinessRules : BaseBusinessRules
{
    private readonly IVariantRepository _variantRepository;
    private readonly ILocalizationService _localizationService;

    public VariantBusinessRules(IVariantRepository variantRepository, ILocalizationService localizationService)
    {
        _variantRepository = variantRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, VariantsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task VariantShouldExistWhenSelected(Variant? variant)
    {
        if (variant == null)
            await throwBusinessException(VariantsBusinessMessages.VariantNotExists);
    }

    public async Task VariantIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetAsync(
            predicate: v => v.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await VariantShouldExistWhenSelected(variant);
    }
    public async Task VariantNameIsAlreadyExist(Variant? variant)
    {
        if (variant != null)
            await throwBusinessException(VariantsBusinessMessages.VariantNameIsAlreadyExist);
    }

    public async Task VariantNameShouldNotRepeat(string name, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetAsync(
            predicate: v => v.VariantName == name,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await VariantNameIsAlreadyExist(variant);
    }
    public async Task VariantIsTopVariant(Variant? variant)
    {
        if (variant != null)
            await throwBusinessException(VariantsBusinessMessages.VariantIsTopVariant);
    }

    public async Task VariantShouldNotTopVariant(int id, CancellationToken cancellationToken)
    {
        Variant? variant = await _variantRepository.GetAsync(
            predicate: v => v.TopVariantId == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await VariantIsTopVariant(variant);
    }
}
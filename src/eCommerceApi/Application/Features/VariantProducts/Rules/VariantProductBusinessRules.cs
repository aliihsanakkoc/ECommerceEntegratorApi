using Application.Features.VariantProducts.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.VariantProducts.Rules;

public class VariantProductBusinessRules : BaseBusinessRules
{
    private readonly IVariantProductRepository _variantProductRepository;
    private readonly ILocalizationService _localizationService;

    public VariantProductBusinessRules(IVariantProductRepository variantProductRepository, ILocalizationService localizationService)
    {
        _variantProductRepository = variantProductRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, VariantProductsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task VariantProductShouldExistWhenSelected(VariantProduct? variantProduct)
    {
        if (variantProduct == null)
            await throwBusinessException(VariantProductsBusinessMessages.VariantProductNotExists);
    }

    public async Task VariantProductIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        VariantProduct? variantProduct = await _variantProductRepository.GetAsync(
            predicate: vp => vp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await VariantProductShouldExistWhenSelected(variantProduct);
    }
}
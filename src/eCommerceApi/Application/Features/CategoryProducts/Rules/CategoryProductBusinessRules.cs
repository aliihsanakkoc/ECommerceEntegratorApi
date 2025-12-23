using Application.Features.CategoryProducts.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.CategoryProducts.Rules;

public class CategoryProductBusinessRules : BaseBusinessRules
{
    private readonly ICategoryProductRepository _categoryProductRepository;
    private readonly ILocalizationService _localizationService;

    public CategoryProductBusinessRules(
        ICategoryProductRepository categoryProductRepository,
        ILocalizationService localizationService
    )
    {
        _categoryProductRepository = categoryProductRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CategoryProductsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CategoryProductShouldExistWhenSelected(CategoryProduct? categoryProduct)
    {
        if (categoryProduct == null)
            await throwBusinessException(CategoryProductsBusinessMessages.CategoryProductNotExists);
    }

    public async Task CategoryProductIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        CategoryProduct? categoryProduct = await _categoryProductRepository.GetAsync(
            predicate: cp => cp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CategoryProductShouldExistWhenSelected(categoryProduct);
    }
}

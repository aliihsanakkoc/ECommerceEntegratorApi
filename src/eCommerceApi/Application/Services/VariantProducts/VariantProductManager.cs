using Application.Features.VariantProducts.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.VariantProducts;

public class VariantProductManager : IVariantProductService
{
    private readonly IVariantProductRepository _variantProductRepository;
    private readonly VariantProductBusinessRules _variantProductBusinessRules;

    public VariantProductManager(IVariantProductRepository variantProductRepository, VariantProductBusinessRules variantProductBusinessRules)
    {
        _variantProductRepository = variantProductRepository;
        _variantProductBusinessRules = variantProductBusinessRules;
    }

    public async Task<VariantProduct?> GetAsync(
        Expression<Func<VariantProduct, bool>> predicate,
        Func<IQueryable<VariantProduct>, IIncludableQueryable<VariantProduct, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        VariantProduct? variantProduct = await _variantProductRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return variantProduct;
    }

    public async Task<IPaginate<VariantProduct>?> GetListAsync(
        Expression<Func<VariantProduct, bool>>? predicate = null,
        Func<IQueryable<VariantProduct>, IOrderedQueryable<VariantProduct>>? orderBy = null,
        Func<IQueryable<VariantProduct>, IIncludableQueryable<VariantProduct, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<VariantProduct> variantProductList = await _variantProductRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return variantProductList;
    }

    public async Task<VariantProduct> AddAsync(VariantProduct variantProduct)
    {
        VariantProduct addedVariantProduct = await _variantProductRepository.AddAsync(variantProduct);

        return addedVariantProduct;
    }

    public async Task<VariantProduct> UpdateAsync(VariantProduct variantProduct)
    {
        VariantProduct updatedVariantProduct = await _variantProductRepository.UpdateAsync(variantProduct);

        return updatedVariantProduct;
    }

    public async Task<VariantProduct> DeleteAsync(VariantProduct variantProduct, bool permanent = false)
    {
        VariantProduct deletedVariantProduct = await _variantProductRepository.DeleteAsync(variantProduct);

        return deletedVariantProduct;
    }
}

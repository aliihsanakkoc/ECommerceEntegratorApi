using System.Linq.Expressions;
using Application.Features.Variants.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Variants;

public class VariantManager : IVariantService
{
    private readonly IVariantRepository _variantRepository;
    private readonly VariantBusinessRules _variantBusinessRules;

    public VariantManager(IVariantRepository variantRepository, VariantBusinessRules variantBusinessRules)
    {
        _variantRepository = variantRepository;
        _variantBusinessRules = variantBusinessRules;
    }

    public async Task<Variant?> GetAsync(
        Expression<Func<Variant, bool>> predicate,
        Func<IQueryable<Variant>, IIncludableQueryable<Variant, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Variant? variant = await _variantRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return variant;
    }

    public async Task<IPaginate<Variant>?> GetListAsync(
        Expression<Func<Variant, bool>>? predicate = null,
        Func<IQueryable<Variant>, IOrderedQueryable<Variant>>? orderBy = null,
        Func<IQueryable<Variant>, IIncludableQueryable<Variant, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Variant> variantList = await _variantRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return variantList;
    }

    public async Task<Variant> AddAsync(Variant variant)
    {
        Variant addedVariant = await _variantRepository.AddAsync(variant);

        return addedVariant;
    }

    public async Task<Variant> UpdateAsync(Variant variant)
    {
        Variant updatedVariant = await _variantRepository.UpdateAsync(variant);

        return updatedVariant;
    }

    public async Task<Variant> DeleteAsync(Variant variant, bool permanent = false)
    {
        Variant deletedVariant = await _variantRepository.DeleteAsync(variant);

        return deletedVariant;
    }
}

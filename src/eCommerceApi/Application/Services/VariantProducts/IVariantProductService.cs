using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.VariantProducts;

public interface IVariantProductService
{
    Task<VariantProduct?> GetAsync(
        Expression<Func<VariantProduct, bool>> predicate,
        Func<IQueryable<VariantProduct>, IIncludableQueryable<VariantProduct, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<VariantProduct>?> GetListAsync(
        Expression<Func<VariantProduct, bool>>? predicate = null,
        Func<IQueryable<VariantProduct>, IOrderedQueryable<VariantProduct>>? orderBy = null,
        Func<IQueryable<VariantProduct>, IIncludableQueryable<VariantProduct, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<VariantProduct> AddAsync(VariantProduct variantProduct);
    Task<VariantProduct> UpdateAsync(VariantProduct variantProduct);
    Task<VariantProduct> DeleteAsync(VariantProduct variantProduct, bool permanent = false);
}

using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CategoryProducts;

public interface ICategoryProductService
{
    Task<CategoryProduct?> GetAsync(
        Expression<Func<CategoryProduct, bool>> predicate,
        Func<IQueryable<CategoryProduct>, IIncludableQueryable<CategoryProduct, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CategoryProduct>?> GetListAsync(
        Expression<Func<CategoryProduct, bool>>? predicate = null,
        Func<IQueryable<CategoryProduct>, IOrderedQueryable<CategoryProduct>>? orderBy = null,
        Func<IQueryable<CategoryProduct>, IIncludableQueryable<CategoryProduct, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CategoryProduct> AddAsync(CategoryProduct categoryProduct);
    Task<CategoryProduct> UpdateAsync(CategoryProduct categoryProduct);
    Task<CategoryProduct> DeleteAsync(CategoryProduct categoryProduct, bool permanent = false);
}

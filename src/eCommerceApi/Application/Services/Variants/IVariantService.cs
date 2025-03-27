using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Variants;

public interface IVariantService
{
    Task<Variant?> GetAsync(
        Expression<Func<Variant, bool>> predicate,
        Func<IQueryable<Variant>, IIncludableQueryable<Variant, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Variant>?> GetListAsync(
        Expression<Func<Variant, bool>>? predicate = null,
        Func<IQueryable<Variant>, IOrderedQueryable<Variant>>? orderBy = null,
        Func<IQueryable<Variant>, IIncludableQueryable<Variant, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Variant> AddAsync(Variant variant);
    Task<Variant> UpdateAsync(Variant variant);
    Task<Variant> DeleteAsync(Variant variant, bool permanent = false);
}

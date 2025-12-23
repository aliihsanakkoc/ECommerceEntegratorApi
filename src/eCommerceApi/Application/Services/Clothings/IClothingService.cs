using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Clothings;

public interface IClothingService
{
    Task<Clothing?> GetAsync(
        Expression<Func<Clothing, bool>> predicate,
        Func<IQueryable<Clothing>, IIncludableQueryable<Clothing, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Clothing>?> GetListAsync(
        Expression<Func<Clothing, bool>>? predicate = null,
        Func<IQueryable<Clothing>, IOrderedQueryable<Clothing>>? orderBy = null,
        Func<IQueryable<Clothing>, IIncludableQueryable<Clothing, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Clothing> AddAsync(Clothing clothing);
    Task<Clothing> UpdateAsync(Clothing clothing);
    Task<Clothing> DeleteAsync(Clothing clothing, bool permanent = false);
}

using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Foods;

public interface IFoodService
{
    Task<Food?> GetAsync(
        Expression<Func<Food, bool>> predicate,
        Func<IQueryable<Food>, IIncludableQueryable<Food, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Food>?> GetListAsync(
        Expression<Func<Food, bool>>? predicate = null,
        Func<IQueryable<Food>, IOrderedQueryable<Food>>? orderBy = null,
        Func<IQueryable<Food>, IIncludableQueryable<Food, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Food> AddAsync(Food food);
    Task<Food> UpdateAsync(Food food);
    Task<Food> DeleteAsync(Food food, bool permanent = false);
}

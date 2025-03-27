using Application.Features.Foods.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Foods;

public class FoodManager : IFoodService
{
    private readonly IFoodRepository _foodRepository;
    private readonly FoodBusinessRules _foodBusinessRules;

    public FoodManager(IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules)
    {
        _foodRepository = foodRepository;
        _foodBusinessRules = foodBusinessRules;
    }

    public async Task<Food?> GetAsync(
        Expression<Func<Food, bool>> predicate,
        Func<IQueryable<Food>, IIncludableQueryable<Food, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Food? food = await _foodRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return food;
    }

    public async Task<IPaginate<Food>?> GetListAsync(
        Expression<Func<Food, bool>>? predicate = null,
        Func<IQueryable<Food>, IOrderedQueryable<Food>>? orderBy = null,
        Func<IQueryable<Food>, IIncludableQueryable<Food, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Food> foodList = await _foodRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return foodList;
    }

    public async Task<Food> AddAsync(Food food)
    {
        Food addedFood = await _foodRepository.AddAsync(food);

        return addedFood;
    }

    public async Task<Food> UpdateAsync(Food food)
    {
        Food updatedFood = await _foodRepository.UpdateAsync(food);

        return updatedFood;
    }

    public async Task<Food> DeleteAsync(Food food, bool permanent = false)
    {
        Food deletedFood = await _foodRepository.DeleteAsync(food);

        return deletedFood;
    }
}

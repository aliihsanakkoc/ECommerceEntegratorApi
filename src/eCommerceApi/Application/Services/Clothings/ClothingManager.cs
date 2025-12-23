using System.Linq.Expressions;
using Application.Features.Clothings.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Clothings;

public class ClothingManager : IClothingService
{
    private readonly IClothingRepository _clothingRepository;
    private readonly ClothingBusinessRules _clothingBusinessRules;

    public ClothingManager(IClothingRepository clothingRepository, ClothingBusinessRules clothingBusinessRules)
    {
        _clothingRepository = clothingRepository;
        _clothingBusinessRules = clothingBusinessRules;
    }

    public async Task<Clothing?> GetAsync(
        Expression<Func<Clothing, bool>> predicate,
        Func<IQueryable<Clothing>, IIncludableQueryable<Clothing, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Clothing? clothing = await _clothingRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return clothing;
    }

    public async Task<IPaginate<Clothing>?> GetListAsync(
        Expression<Func<Clothing, bool>>? predicate = null,
        Func<IQueryable<Clothing>, IOrderedQueryable<Clothing>>? orderBy = null,
        Func<IQueryable<Clothing>, IIncludableQueryable<Clothing, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Clothing> clothingList = await _clothingRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return clothingList;
    }

    public async Task<Clothing> AddAsync(Clothing clothing)
    {
        Clothing addedClothing = await _clothingRepository.AddAsync(clothing);

        return addedClothing;
    }

    public async Task<Clothing> UpdateAsync(Clothing clothing)
    {
        Clothing updatedClothing = await _clothingRepository.UpdateAsync(clothing);

        return updatedClothing;
    }

    public async Task<Clothing> DeleteAsync(Clothing clothing, bool permanent = false)
    {
        Clothing deletedClothing = await _clothingRepository.DeleteAsync(clothing);

        return deletedClothing;
    }
}

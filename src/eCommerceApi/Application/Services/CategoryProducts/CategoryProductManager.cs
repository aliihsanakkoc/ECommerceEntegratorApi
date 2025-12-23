using System.Linq.Expressions;
using Application.Features.CategoryProducts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.CategoryProducts;

public class CategoryProductManager : ICategoryProductService
{
    private readonly ICategoryProductRepository _categoryProductRepository;
    private readonly CategoryProductBusinessRules _categoryProductBusinessRules;

    public CategoryProductManager(
        ICategoryProductRepository categoryProductRepository,
        CategoryProductBusinessRules categoryProductBusinessRules
    )
    {
        _categoryProductRepository = categoryProductRepository;
        _categoryProductBusinessRules = categoryProductBusinessRules;
    }

    public async Task<CategoryProduct?> GetAsync(
        Expression<Func<CategoryProduct, bool>> predicate,
        Func<IQueryable<CategoryProduct>, IIncludableQueryable<CategoryProduct, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CategoryProduct? categoryProduct = await _categoryProductRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return categoryProduct;
    }

    public async Task<IPaginate<CategoryProduct>?> GetListAsync(
        Expression<Func<CategoryProduct, bool>>? predicate = null,
        Func<IQueryable<CategoryProduct>, IOrderedQueryable<CategoryProduct>>? orderBy = null,
        Func<IQueryable<CategoryProduct>, IIncludableQueryable<CategoryProduct, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CategoryProduct> categoryProductList = await _categoryProductRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return categoryProductList;
    }

    public async Task<CategoryProduct> AddAsync(CategoryProduct categoryProduct)
    {
        CategoryProduct addedCategoryProduct = await _categoryProductRepository.AddAsync(categoryProduct);

        return addedCategoryProduct;
    }

    public async Task<CategoryProduct> UpdateAsync(CategoryProduct categoryProduct)
    {
        CategoryProduct updatedCategoryProduct = await _categoryProductRepository.UpdateAsync(categoryProduct);

        return updatedCategoryProduct;
    }

    public async Task<CategoryProduct> DeleteAsync(CategoryProduct categoryProduct, bool permanent = false)
    {
        CategoryProduct deletedCategoryProduct = await _categoryProductRepository.DeleteAsync(categoryProduct);

        return deletedCategoryProduct;
    }
}

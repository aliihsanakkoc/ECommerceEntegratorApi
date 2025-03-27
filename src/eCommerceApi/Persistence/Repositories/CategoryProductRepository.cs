using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryProductRepository : EfRepositoryBase<CategoryProduct, int, BaseDbContext>, ICategoryProductRepository
{
    public CategoryProductRepository(BaseDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FoodRepository : EfRepositoryBase<Food, int, BaseDbContext>, IFoodRepository
{
    public FoodRepository(BaseDbContext context) : base(context)
    {
    }
}
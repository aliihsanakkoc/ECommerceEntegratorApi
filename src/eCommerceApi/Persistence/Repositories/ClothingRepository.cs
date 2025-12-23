using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClothingRepository : EfRepositoryBase<Clothing, int, BaseDbContext>, IClothingRepository
{
    public ClothingRepository(BaseDbContext context)
        : base(context) { }
}

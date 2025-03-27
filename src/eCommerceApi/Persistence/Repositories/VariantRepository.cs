using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class VariantRepository : EfRepositoryBase<Variant, int, BaseDbContext>, IVariantRepository
{
    public VariantRepository(BaseDbContext context) : base(context)
    {
    }
}
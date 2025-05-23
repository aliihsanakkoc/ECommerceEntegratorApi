using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class VariantProductRepository : EfRepositoryBase<VariantProduct, int, BaseDbContext>, IVariantProductRepository
{
    public VariantProductRepository(BaseDbContext context) : base(context)
    {
    }
}
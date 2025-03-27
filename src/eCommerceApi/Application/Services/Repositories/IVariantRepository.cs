using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IVariantRepository : IAsyncRepository<Variant, int>, IRepository<Variant, int>
{
}
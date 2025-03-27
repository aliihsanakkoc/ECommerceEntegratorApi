using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICategoryProductRepository : IAsyncRepository<CategoryProduct, int>, IRepository<CategoryProduct, int>
{
}
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFoodRepository : IAsyncRepository<Food, int>, IRepository<Food, int>
{
}
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClothingRepository : IAsyncRepository<Clothing, int>, IRepository<Clothing, int> { }

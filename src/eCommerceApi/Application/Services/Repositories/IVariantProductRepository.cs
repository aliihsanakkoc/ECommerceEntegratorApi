using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IVariantProductRepository : IAsyncRepository<VariantProduct, int>, IRepository<VariantProduct, int> { }

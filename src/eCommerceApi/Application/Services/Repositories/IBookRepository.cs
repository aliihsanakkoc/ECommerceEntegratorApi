using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBookRepository : IAsyncRepository<Book, int>, IRepository<Book, int>
{
}
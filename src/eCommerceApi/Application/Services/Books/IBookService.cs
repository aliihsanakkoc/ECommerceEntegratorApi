using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Books;

public interface IBookService
{
    Task<Book?> GetAsync(
        Expression<Func<Book, bool>> predicate,
        Func<IQueryable<Book>, IIncludableQueryable<Book, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Book>?> GetListAsync(
        Expression<Func<Book, bool>>? predicate = null,
        Func<IQueryable<Book>, IOrderedQueryable<Book>>? orderBy = null,
        Func<IQueryable<Book>, IIncludableQueryable<Book, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Book> AddAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<Book> DeleteAsync(Book book, bool permanent = false);
}

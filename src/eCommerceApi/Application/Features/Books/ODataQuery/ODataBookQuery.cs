using Application.Features.Books.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Books.Constants.BooksOperationClaims;

namespace Application.Features.Books.ODataQuery;
public class ODataBookQuery : IRequest<IQueryable<GetListBookListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin];
}
public class ODataBookQueryHandler : IRequestHandler<ODataBookQuery, IQueryable<GetListBookListItemDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    public ODataBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    public Task<IQueryable<GetListBookListItemDto>> Handle(ODataBookQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Book> books = _bookRepository.Query();
        return Task.FromResult(books.ProjectTo<GetListBookListItemDto>(_mapper.ConfigurationProvider));
    }
}
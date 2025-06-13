using Application.Features.Products.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Products.Constants.ProductsOperationClaims;

namespace Application.Features.Products.ODataQuery;
public class ODataProductQuery : IRequest<IQueryable<GetListProductListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin];
    public class ODataProductQueryHandler : IRequestHandler<ODataProductQuery, IQueryable<GetListProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ODataProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public Task<IQueryable<GetListProductListItemDto>> Handle(ODataProductQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = _productRepository.Query();
            return Task.FromResult(products.ProjectTo<GetListProductListItemDto>(_mapper.ConfigurationProvider));
        }
    }
}

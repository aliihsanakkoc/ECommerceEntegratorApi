using Application.Features.CategoryProducts.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.CategoryProducts.Constants.CategoryProductsOperationClaims;
namespace Application.Features.CategoryProducts.ODataQuery;
public class ODataCategoryProductQuery : IRequest<IQueryable<GetListCategoryProductListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin,Read];
    public class ODataCategoryProductQueryHandler : IRequestHandler<ODataCategoryProductQuery, IQueryable<GetListCategoryProductListItemDto>>
    {
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IMapper _mapper;
        public ODataCategoryProductQueryHandler(ICategoryProductRepository categoryProductRepository, IMapper mapper)
        {
            _categoryProductRepository = categoryProductRepository;
            _mapper = mapper;
        }
        public Task<IQueryable<GetListCategoryProductListItemDto>> Handle(ODataCategoryProductQuery request, CancellationToken cancellationToken)
        {
            IQueryable<CategoryProduct> categoryProducts = _categoryProductRepository.Query();
            return Task.FromResult(categoryProducts.ProjectTo<GetListCategoryProductListItemDto>(_mapper.ConfigurationProvider)); 
        }
    }
}

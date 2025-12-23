using Application.Features.VariantProducts.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;

namespace Application.Features.VariantProducts.ODataQuery;

public class ODataVariantProductQuery : IRequest<IQueryable<GetListVariantProductListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin, Read, Client];

    public class ODataVariantProductQueryHandler
        : IRequestHandler<ODataVariantProductQuery, IQueryable<GetListVariantProductListItemDto>>
    {
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly IMapper _mapper;

        public ODataVariantProductQueryHandler(IVariantProductRepository variantProductRepository, IMapper mapper)
        {
            _variantProductRepository = variantProductRepository;
            _mapper = mapper;
        }

        public Task<IQueryable<GetListVariantProductListItemDto>> Handle(
            ODataVariantProductQuery request,
            CancellationToken cancellationToken
        )
        {
            IQueryable<VariantProduct> variantProducts = _variantProductRepository.Query();
            return Task.FromResult(variantProducts.ProjectTo<GetListVariantProductListItemDto>(_mapper.ConfigurationProvider));
        }
    }
}

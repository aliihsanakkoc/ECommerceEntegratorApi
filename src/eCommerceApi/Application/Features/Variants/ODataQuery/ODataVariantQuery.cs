using Application.Features.Variants.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Variants.Constants.VariantsOperationClaims;

namespace Application.Features.Variants.ODataQuery;
public class ODataVariantQuery : IRequest<IQueryable<GetListVariantListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin,Read, Client];
    public class ODataVariantQueryHandler : IRequestHandler<ODataVariantQuery, IQueryable<GetListVariantListItemDto>>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;

        public ODataVariantQueryHandler(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }

        public Task<IQueryable<GetListVariantListItemDto>> Handle(ODataVariantQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Variant> variants = _variantRepository.Query();

            return Task.FromResult(variants.ProjectTo<GetListVariantListItemDto>(_mapper.ConfigurationProvider));
        }
    }   
}

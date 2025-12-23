using Application.Features.Clothings.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Clothings.Constants.ClothingsOperationClaims;
namespace Application.Features.Clothings.ODataQuery;
public class ODataClothingQuery : IRequest<IQueryable<GetListClothingListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin, Client];
    public class ODataClothingQueryHandler : IRequestHandler<ODataClothingQuery, IQueryable<GetListClothingListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IClothingRepository _clothingRepository;
        public ODataClothingQueryHandler(IMapper mapper, IClothingRepository clothingRepository)
        {
            _mapper = mapper;
            _clothingRepository = clothingRepository;
        }
        public Task<IQueryable<GetListClothingListItemDto>> Handle(ODataClothingQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Clothing> clothings = _clothingRepository.Query();
            return Task.FromResult(clothings.ProjectTo<GetListClothingListItemDto>(_mapper.ConfigurationProvider));
        }
    }
}

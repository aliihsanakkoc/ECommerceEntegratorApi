using Application.Features.Categories.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Categories.Constants.CategoriesOperationClaims;


namespace Application.Features.Categories.ODataQuery;
public class ODataCategoryQuery : IRequest<IQueryable<GetListCategoryListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin, Read, Client];
    public class ODataCategoryQueryHandler : IRequestHandler<ODataCategoryQuery, IQueryable<GetListCategoryListItemDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ODataCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public Task<IQueryable<GetListCategoryListItemDto>> Handle(ODataCategoryQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Category> categories = _categoryRepository.Query();
            return Task.FromResult(categories.ProjectTo<GetListCategoryListItemDto>(_mapper.ConfigurationProvider));    
        }
    }
}

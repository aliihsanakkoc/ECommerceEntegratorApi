using Application.Features.CategoryProducts.Constants;
using Application.Features.CategoryProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.CategoryProducts.Constants.CategoryProductsOperationClaims;

namespace Application.Features.CategoryProducts.Queries.GetById;

public class GetByIdCategoryProductQuery : IRequest<GetByIdCategoryProductResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCategoryProductQueryHandler : IRequestHandler<GetByIdCategoryProductQuery, GetByIdCategoryProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly CategoryProductBusinessRules _categoryProductBusinessRules;

        public GetByIdCategoryProductQueryHandler(IMapper mapper, ICategoryProductRepository categoryProductRepository, CategoryProductBusinessRules categoryProductBusinessRules)
        {
            _mapper = mapper;
            _categoryProductRepository = categoryProductRepository;
            _categoryProductBusinessRules = categoryProductBusinessRules;
        }

        public async Task<GetByIdCategoryProductResponse> Handle(GetByIdCategoryProductQuery request, CancellationToken cancellationToken)
        {
            CategoryProduct? categoryProduct = await _categoryProductRepository.GetAsync(predicate: cp => cp.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryProductBusinessRules.CategoryProductShouldExistWhenSelected(categoryProduct);

            GetByIdCategoryProductResponse response = _mapper.Map<GetByIdCategoryProductResponse>(categoryProduct);
            return response;
        }
    }
}
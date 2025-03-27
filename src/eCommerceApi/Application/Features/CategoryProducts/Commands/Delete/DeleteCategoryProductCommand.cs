using Application.Features.CategoryProducts.Constants;
using Application.Features.CategoryProducts.Constants;
using Application.Features.CategoryProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CategoryProducts.Constants.CategoryProductsOperationClaims;

namespace Application.Features.CategoryProducts.Commands.Delete;

public class DeleteCategoryProductCommand : IRequest<DeletedCategoryProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, CategoryProductsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryProducts"];

    public class DeleteCategoryProductCommandHandler : IRequestHandler<DeleteCategoryProductCommand, DeletedCategoryProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly CategoryProductBusinessRules _categoryProductBusinessRules;

        public DeleteCategoryProductCommandHandler(IMapper mapper, ICategoryProductRepository categoryProductRepository,
                                         CategoryProductBusinessRules categoryProductBusinessRules)
        {
            _mapper = mapper;
            _categoryProductRepository = categoryProductRepository;
            _categoryProductBusinessRules = categoryProductBusinessRules;
        }

        public async Task<DeletedCategoryProductResponse> Handle(DeleteCategoryProductCommand request, CancellationToken cancellationToken)
        {
            CategoryProduct? categoryProduct = await _categoryProductRepository.GetAsync(predicate: cp => cp.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryProductBusinessRules.CategoryProductShouldExistWhenSelected(categoryProduct);

            await _categoryProductRepository.DeleteAsync(categoryProduct!);

            DeletedCategoryProductResponse response = _mapper.Map<DeletedCategoryProductResponse>(categoryProduct);
            return response;
        }
    }
}
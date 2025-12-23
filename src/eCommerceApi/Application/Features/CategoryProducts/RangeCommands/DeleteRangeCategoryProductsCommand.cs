using Application.Features.CategoryProducts.Constants;
using Application.Features.CategoryProducts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.CategoryProducts.Constants.CategoryProductsOperationClaims;

namespace Application.Features.CategoryProducts.RangeCommands;

public class DeleteRangeCategoryProductsCommand
    : IRequest<Unit>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int[] Ids { get; set; }

    public string[] Roles => [Admin, Write, CategoryProductsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryProducts"];

    public class DeleteRangeCategoryProductsCommandHandler : IRequestHandler<DeleteRangeCategoryProductsCommand, Unit>
    {
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly CategoryProductBusinessRules _categoryProductBusinessRules;

        public DeleteRangeCategoryProductsCommandHandler(
            ICategoryProductRepository categoryProductRepository,
            CategoryProductBusinessRules categoryProductBusinessRules
        )
        {
            _categoryProductRepository = categoryProductRepository;
            _categoryProductBusinessRules = categoryProductBusinessRules;
        }

        public async Task<Unit> Handle(DeleteRangeCategoryProductsCommand request, CancellationToken cancellationToken)
        {
            foreach (int id in request.Ids)
            {
                CategoryProduct? categoryProduct = await _categoryProductRepository.GetAsync(
                    predicate: cp => cp.Id == id,
                    cancellationToken: cancellationToken
                );
                await _categoryProductBusinessRules.CategoryProductShouldExistWhenSelected(categoryProduct);

                await _categoryProductRepository.DeleteAsync(categoryProduct!, permanent: true);
            }
            return Unit.Value;
        }
    }
}

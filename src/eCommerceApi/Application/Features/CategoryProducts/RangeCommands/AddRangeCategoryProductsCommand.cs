using Application.Features.CategoryProducts.Constants;
using Application.Features.CategoryProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.CategoryProducts.Constants.CategoryProductsOperationClaims;

namespace Application.Features.CategoryProducts.RangeCommands;

public class AddRangeCategoryProductsCommand
    : IRequest<Unit>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public required int[] CategoryIds { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, CategoryProductsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryProducts"];

    public class AddRangeCategoryProductsCommandHandler : IRequestHandler<AddRangeCategoryProductsCommand, Unit>
    {
        private readonly ICategoryProductRepository _categoryProductRepository;

        public AddRangeCategoryProductsCommandHandler(ICategoryProductRepository categoryProductRepository)
        {
            _categoryProductRepository = categoryProductRepository;
        }

        public async Task<Unit> Handle(AddRangeCategoryProductsCommand request, CancellationToken cancellationToken)
        {
            List<CategoryProduct> categoryProducts = new();
            foreach (int categoryId in request.CategoryIds)
            {
                categoryProducts.Add(new CategoryProduct { CategoryId = categoryId, ProductId = request.ProductId });
            }

            await _categoryProductRepository.AddRangeAsync(categoryProducts);

            return Unit.Value;
        }
    }
}

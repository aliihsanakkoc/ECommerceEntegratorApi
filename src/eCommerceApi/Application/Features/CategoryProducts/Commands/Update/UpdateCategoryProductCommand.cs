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

namespace Application.Features.CategoryProducts.Commands.Update;

public class UpdateCategoryProductCommand : IRequest<UpdatedCategoryProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required int CategoryId { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, CategoryProductsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryProducts"];

    public class UpdateCategoryProductCommandHandler : IRequestHandler<UpdateCategoryProductCommand, UpdatedCategoryProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly CategoryProductBusinessRules _categoryProductBusinessRules;

        public UpdateCategoryProductCommandHandler(IMapper mapper, ICategoryProductRepository categoryProductRepository,
                                         CategoryProductBusinessRules categoryProductBusinessRules)
        {
            _mapper = mapper;
            _categoryProductRepository = categoryProductRepository;
            _categoryProductBusinessRules = categoryProductBusinessRules;
        }

        public async Task<UpdatedCategoryProductResponse> Handle(UpdateCategoryProductCommand request, CancellationToken cancellationToken)
        {
            CategoryProduct? categoryProduct = await _categoryProductRepository.GetAsync(predicate: cp => cp.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryProductBusinessRules.CategoryProductShouldExistWhenSelected(categoryProduct);
            categoryProduct = _mapper.Map(request, categoryProduct);

            await _categoryProductRepository.UpdateAsync(categoryProduct!);

            UpdatedCategoryProductResponse response = _mapper.Map<UpdatedCategoryProductResponse>(categoryProduct);
            return response;
        }
    }
}
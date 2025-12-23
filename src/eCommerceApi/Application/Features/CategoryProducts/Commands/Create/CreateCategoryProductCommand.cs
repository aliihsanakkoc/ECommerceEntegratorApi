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

namespace Application.Features.CategoryProducts.Commands.Create;

public class CreateCategoryProductCommand
    : IRequest<CreatedCategoryProductResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public required int CategoryId { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, CategoryProductsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryProducts"];

    public class CreateCategoryProductCommandHandler
        : IRequestHandler<CreateCategoryProductCommand, CreatedCategoryProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly CategoryProductBusinessRules _categoryProductBusinessRules;

        public CreateCategoryProductCommandHandler(
            IMapper mapper,
            ICategoryProductRepository categoryProductRepository,
            CategoryProductBusinessRules categoryProductBusinessRules
        )
        {
            _mapper = mapper;
            _categoryProductRepository = categoryProductRepository;
            _categoryProductBusinessRules = categoryProductBusinessRules;
        }

        public async Task<CreatedCategoryProductResponse> Handle(
            CreateCategoryProductCommand request,
            CancellationToken cancellationToken
        )
        {
            CategoryProduct categoryProduct = _mapper.Map<CategoryProduct>(request);

            await _categoryProductRepository.AddAsync(categoryProduct);

            CreatedCategoryProductResponse response = _mapper.Map<CreatedCategoryProductResponse>(categoryProduct);
            return response;
        }
    }
}

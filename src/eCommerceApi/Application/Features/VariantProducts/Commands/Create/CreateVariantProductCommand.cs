using Application.Features.VariantProducts.Constants;
using Application.Features.VariantProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;

namespace Application.Features.VariantProducts.Commands.Create;

public class CreateVariantProductCommand
    : IRequest<CreatedVariantProductResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public required int VariantId { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, VariantProductsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariantProducts"];

    public class CreateVariantProductCommandHandler : IRequestHandler<CreateVariantProductCommand, CreatedVariantProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly VariantProductBusinessRules _variantProductBusinessRules;

        public CreateVariantProductCommandHandler(
            IMapper mapper,
            IVariantProductRepository variantProductRepository,
            VariantProductBusinessRules variantProductBusinessRules
        )
        {
            _mapper = mapper;
            _variantProductRepository = variantProductRepository;
            _variantProductBusinessRules = variantProductBusinessRules;
        }

        public async Task<CreatedVariantProductResponse> Handle(
            CreateVariantProductCommand request,
            CancellationToken cancellationToken
        )
        {
            VariantProduct variantProduct = _mapper.Map<VariantProduct>(request);

            await _variantProductRepository.AddAsync(variantProduct);

            CreatedVariantProductResponse response = _mapper.Map<CreatedVariantProductResponse>(variantProduct);
            return response;
        }
    }
}

using Application.Features.VariantProducts.Constants;
using Application.Features.VariantProducts.Constants;
using Application.Features.VariantProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;

namespace Application.Features.VariantProducts.Commands.Delete;

public class DeleteVariantProductCommand : IRequest<DeletedVariantProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, VariantProductsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariantProducts"];

    public class DeleteVariantProductCommandHandler : IRequestHandler<DeleteVariantProductCommand, DeletedVariantProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly VariantProductBusinessRules _variantProductBusinessRules;

        public DeleteVariantProductCommandHandler(IMapper mapper, IVariantProductRepository variantProductRepository,
                                         VariantProductBusinessRules variantProductBusinessRules)
        {
            _mapper = mapper;
            _variantProductRepository = variantProductRepository;
            _variantProductBusinessRules = variantProductBusinessRules;
        }

        public async Task<DeletedVariantProductResponse> Handle(DeleteVariantProductCommand request, CancellationToken cancellationToken)
        {
            VariantProduct? variantProduct = await _variantProductRepository.GetAsync(predicate: vp => vp.Id == request.Id, cancellationToken: cancellationToken);
            await _variantProductBusinessRules.VariantProductShouldExistWhenSelected(variantProduct);

            await _variantProductRepository.DeleteAsync(variantProduct!);

            DeletedVariantProductResponse response = _mapper.Map<DeletedVariantProductResponse>(variantProduct);
            return response;
        }
    }
}
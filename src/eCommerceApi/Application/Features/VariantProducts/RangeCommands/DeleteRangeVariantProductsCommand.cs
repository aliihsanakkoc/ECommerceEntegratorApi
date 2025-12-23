using Application.Features.VariantProducts.Constants;
using Application.Features.VariantProducts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;

namespace Application.Features.VariantProducts.RangeCommands;

public class DeleteRangeVariantProductsCommand
    : IRequest<Unit>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int[] Ids { get; set; }

    public string[] Roles => [Admin, Write, VariantProductsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariantProducts"];

    public class DeleteRangeVariantProductsCommandHandler : IRequestHandler<DeleteRangeVariantProductsCommand, Unit>
    {
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly VariantProductBusinessRules _variantProductBusinessRules;

        public DeleteRangeVariantProductsCommandHandler(
            IVariantProductRepository variantProductRepository,
            VariantProductBusinessRules variantProductBusinessRules
        )
        {
            _variantProductRepository = variantProductRepository;
            _variantProductBusinessRules = variantProductBusinessRules;
        }

        public async Task<Unit> Handle(DeleteRangeVariantProductsCommand request, CancellationToken cancellationToken)
        {
            foreach (int id in request.Ids)
            {
                VariantProduct? variantProduct = await _variantProductRepository.GetAsync(
                    predicate: vp => vp.Id == id,
                    cancellationToken: cancellationToken
                );
                await _variantProductBusinessRules.VariantProductShouldExistWhenSelected(variantProduct);

                await _variantProductRepository.DeleteAsync(variantProduct!, permanent: true);
            }
            return Unit.Value;
        }
    }
}

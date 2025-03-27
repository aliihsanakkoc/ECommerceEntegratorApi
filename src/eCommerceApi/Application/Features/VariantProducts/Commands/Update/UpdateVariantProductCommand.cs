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

namespace Application.Features.VariantProducts.Commands.Update;

public class UpdateVariantProductCommand : IRequest<UpdatedVariantProductResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required int VariantId { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, VariantProductsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariantProducts"];

    public class UpdateVariantProductCommandHandler : IRequestHandler<UpdateVariantProductCommand, UpdatedVariantProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly VariantProductBusinessRules _variantProductBusinessRules;

        public UpdateVariantProductCommandHandler(IMapper mapper, IVariantProductRepository variantProductRepository,
                                         VariantProductBusinessRules variantProductBusinessRules)
        {
            _mapper = mapper;
            _variantProductRepository = variantProductRepository;
            _variantProductBusinessRules = variantProductBusinessRules;
        }

        public async Task<UpdatedVariantProductResponse> Handle(UpdateVariantProductCommand request, CancellationToken cancellationToken)
        {
            VariantProduct? variantProduct = await _variantProductRepository.GetAsync(predicate: vp => vp.Id == request.Id, cancellationToken: cancellationToken);
            await _variantProductBusinessRules.VariantProductShouldExistWhenSelected(variantProduct);
            variantProduct = _mapper.Map(request, variantProduct);

            await _variantProductRepository.UpdateAsync(variantProduct!);

            UpdatedVariantProductResponse response = _mapper.Map<UpdatedVariantProductResponse>(variantProduct);
            return response;
        }
    }
}
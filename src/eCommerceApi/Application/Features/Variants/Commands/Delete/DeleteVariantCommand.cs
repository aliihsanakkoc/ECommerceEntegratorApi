using Application.Features.Variants.Constants;
using Application.Features.Variants.Constants;
using Application.Features.Variants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Variants.Constants.VariantsOperationClaims;

namespace Application.Features.Variants.Commands.Delete;

public class DeleteVariantCommand
    : IRequest<DeletedVariantResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, VariantsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariants"];

    public class DeleteVariantCommandHandler : IRequestHandler<DeleteVariantCommand, DeletedVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantRepository _variantRepository;
        private readonly VariantBusinessRules _variantBusinessRules;

        public DeleteVariantCommandHandler(
            IMapper mapper,
            IVariantRepository variantRepository,
            VariantBusinessRules variantBusinessRules
        )
        {
            _mapper = mapper;
            _variantRepository = variantRepository;
            _variantBusinessRules = variantBusinessRules;
        }

        public async Task<DeletedVariantResponse> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
        {
            await _variantBusinessRules.VariantShouldNotTopVariant(request.Id, cancellationToken);

            Variant? variant = await _variantRepository.GetAsync(
                predicate: v => v.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _variantBusinessRules.VariantShouldExistWhenSelected(variant);

            await _variantRepository.DeleteAsync(variant!);

            DeletedVariantResponse response = _mapper.Map<DeletedVariantResponse>(variant);
            return response;
        }
    }
}

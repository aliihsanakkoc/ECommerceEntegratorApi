using Application.Features.Variants.Constants;
using Application.Features.Variants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Variants.Constants.VariantsOperationClaims;

namespace Application.Features.Variants.Commands.Update;

public class UpdateVariantCommand : IRequest<UpdatedVariantResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required string VariantName { get; set; }
    public required string TopVariantName { get; set; }
    public required int TopVariantId { get; set; }

    public string[] Roles => [Admin, Write, VariantsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariants"];

    public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, UpdatedVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantRepository _variantRepository;
        private readonly VariantBusinessRules _variantBusinessRules;

        public UpdateVariantCommandHandler(IMapper mapper, IVariantRepository variantRepository,
                                         VariantBusinessRules variantBusinessRules)
        {
            _mapper = mapper;
            _variantRepository = variantRepository;
            _variantBusinessRules = variantBusinessRules;
        }

        public async Task<UpdatedVariantResponse> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            Variant? variant = await _variantRepository.GetAsync(predicate: v => v.Id == request.Id, cancellationToken: cancellationToken);
            await _variantBusinessRules.VariantShouldExistWhenSelected(variant);
            variant = _mapper.Map(request, variant);

            await _variantRepository.UpdateAsync(variant!);

            UpdatedVariantResponse response = _mapper.Map<UpdatedVariantResponse>(variant);
            return response;
        }
    }
}
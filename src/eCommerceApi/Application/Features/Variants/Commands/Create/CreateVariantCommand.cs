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

namespace Application.Features.Variants.Commands.Create;

public class CreateVariantCommand
    : IRequest<CreatedVariantResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public required string VariantName { get; set; }
    public required string TopVariantName { get; set; }
    public required int TopVariantId { get; set; }

    public string[] Roles => [Admin, Write, VariantsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariants"];

    public class CreateVariantCommandHandler : IRequestHandler<CreateVariantCommand, CreatedVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantRepository _variantRepository;
        private readonly VariantBusinessRules _variantBusinessRules;

        public CreateVariantCommandHandler(
            IMapper mapper,
            IVariantRepository variantRepository,
            VariantBusinessRules variantBusinessRules
        )
        {
            _mapper = mapper;
            _variantRepository = variantRepository;
            _variantBusinessRules = variantBusinessRules;
        }

        public async Task<CreatedVariantResponse> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
        {
            await _variantBusinessRules.VariantNameShouldNotRepeat(request.VariantName, cancellationToken);

            Variant variant = _mapper.Map<Variant>(request);

            await _variantRepository.AddAsync(variant);

            CreatedVariantResponse response = _mapper.Map<CreatedVariantResponse>(variant);
            return response;
        }
    }
}

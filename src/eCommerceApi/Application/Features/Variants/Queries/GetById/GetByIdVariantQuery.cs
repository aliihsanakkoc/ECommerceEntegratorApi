using Application.Features.Variants.Constants;
using Application.Features.Variants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Variants.Constants.VariantsOperationClaims;

namespace Application.Features.Variants.Queries.GetById;

public class GetByIdVariantQuery : IRequest<GetByIdVariantResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdVariantQueryHandler : IRequestHandler<GetByIdVariantQuery, GetByIdVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantRepository _variantRepository;
        private readonly VariantBusinessRules _variantBusinessRules;

        public GetByIdVariantQueryHandler(
            IMapper mapper,
            IVariantRepository variantRepository,
            VariantBusinessRules variantBusinessRules
        )
        {
            _mapper = mapper;
            _variantRepository = variantRepository;
            _variantBusinessRules = variantBusinessRules;
        }

        public async Task<GetByIdVariantResponse> Handle(GetByIdVariantQuery request, CancellationToken cancellationToken)
        {
            Variant? variant = await _variantRepository.GetAsync(
                predicate: v => v.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _variantBusinessRules.VariantShouldExistWhenSelected(variant);

            GetByIdVariantResponse response = _mapper.Map<GetByIdVariantResponse>(variant);
            return response;
        }
    }
}

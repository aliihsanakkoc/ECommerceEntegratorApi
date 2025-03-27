using Application.Features.VariantProducts.Constants;
using Application.Features.VariantProducts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;

namespace Application.Features.VariantProducts.Queries.GetById;

public class GetByIdVariantProductQuery : IRequest<GetByIdVariantProductResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdVariantProductQueryHandler : IRequestHandler<GetByIdVariantProductQuery, GetByIdVariantProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVariantProductRepository _variantProductRepository;
        private readonly VariantProductBusinessRules _variantProductBusinessRules;

        public GetByIdVariantProductQueryHandler(IMapper mapper, IVariantProductRepository variantProductRepository, VariantProductBusinessRules variantProductBusinessRules)
        {
            _mapper = mapper;
            _variantProductRepository = variantProductRepository;
            _variantProductBusinessRules = variantProductBusinessRules;
        }

        public async Task<GetByIdVariantProductResponse> Handle(GetByIdVariantProductQuery request, CancellationToken cancellationToken)
        {
            VariantProduct? variantProduct = await _variantProductRepository.GetAsync(predicate: vp => vp.Id == request.Id, cancellationToken: cancellationToken);
            await _variantProductBusinessRules.VariantProductShouldExistWhenSelected(variantProduct);

            GetByIdVariantProductResponse response = _mapper.Map<GetByIdVariantProductResponse>(variantProduct);
            return response;
        }
    }
}
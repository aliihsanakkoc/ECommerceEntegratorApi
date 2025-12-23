using Application.Features.Clothings.Constants;
using Application.Features.Clothings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Clothings.Constants.ClothingsOperationClaims;

namespace Application.Features.Clothings.Queries.GetById;

public class GetByIdClothingQuery : IRequest<GetByIdClothingResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdClothingQueryHandler : IRequestHandler<GetByIdClothingQuery, GetByIdClothingResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClothingRepository _clothingRepository;
        private readonly ClothingBusinessRules _clothingBusinessRules;

        public GetByIdClothingQueryHandler(
            IMapper mapper,
            IClothingRepository clothingRepository,
            ClothingBusinessRules clothingBusinessRules
        )
        {
            _mapper = mapper;
            _clothingRepository = clothingRepository;
            _clothingBusinessRules = clothingBusinessRules;
        }

        public async Task<GetByIdClothingResponse> Handle(GetByIdClothingQuery request, CancellationToken cancellationToken)
        {
            Clothing? clothing = await _clothingRepository.GetAsync(
                predicate: c => c.ProductId == request.Id,
                cancellationToken: cancellationToken
            );
            await _clothingBusinessRules.ClothingShouldExistWhenSelected(clothing);

            GetByIdClothingResponse response = _mapper.Map<GetByIdClothingResponse>(clothing);
            return response;
        }
    }
}

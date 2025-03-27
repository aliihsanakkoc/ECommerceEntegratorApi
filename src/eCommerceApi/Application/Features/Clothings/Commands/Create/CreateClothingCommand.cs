using Application.Features.Clothings.Constants;
using Application.Features.Clothings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Clothings.Constants.ClothingsOperationClaims;

namespace Application.Features.Clothings.Commands.Create;

public class CreateClothingCommand : IRequest<CreatedClothingResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string MadeIn { get; set; }
    public required string FiberComposition { get; set; }
    public string? LaundryLabel { get; set; }
    public string? Brand { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, ClothingsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetClothings"];

    public class CreateClothingCommandHandler : IRequestHandler<CreateClothingCommand, CreatedClothingResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClothingRepository _clothingRepository;
        private readonly ClothingBusinessRules _clothingBusinessRules;

        public CreateClothingCommandHandler(IMapper mapper, IClothingRepository clothingRepository,
                                         ClothingBusinessRules clothingBusinessRules)
        {
            _mapper = mapper;
            _clothingRepository = clothingRepository;
            _clothingBusinessRules = clothingBusinessRules;
        }

        public async Task<CreatedClothingResponse> Handle(CreateClothingCommand request, CancellationToken cancellationToken)
        {
            Clothing clothing = _mapper.Map<Clothing>(request);

            await _clothingRepository.AddAsync(clothing);

            CreatedClothingResponse response = _mapper.Map<CreatedClothingResponse>(clothing);
            return response;
        }
    }
}
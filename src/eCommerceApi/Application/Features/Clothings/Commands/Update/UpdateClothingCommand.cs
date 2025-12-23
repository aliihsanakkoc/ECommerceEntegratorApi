using Application.Features.Clothings.Constants;
using Application.Features.Clothings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Clothings.Constants.ClothingsOperationClaims;

namespace Application.Features.Clothings.Commands.Update;

public class UpdateClothingCommand
    : IRequest<UpdatedClothingResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public required string MadeIn { get; set; }
    public required string FiberComposition { get; set; }
    public string? LaundryLabel { get; set; }
    public string? Brand { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, ClothingsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetClothings"];

    public class UpdateClothingCommandHandler : IRequestHandler<UpdateClothingCommand, UpdatedClothingResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClothingRepository _clothingRepository;
        private readonly ClothingBusinessRules _clothingBusinessRules;

        public UpdateClothingCommandHandler(
            IMapper mapper,
            IClothingRepository clothingRepository,
            ClothingBusinessRules clothingBusinessRules
        )
        {
            _mapper = mapper;
            _clothingRepository = clothingRepository;
            _clothingBusinessRules = clothingBusinessRules;
        }

        public async Task<UpdatedClothingResponse> Handle(UpdateClothingCommand request, CancellationToken cancellationToken)
        {
            Clothing? clothing = await _clothingRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _clothingBusinessRules.ClothingShouldExistWhenSelected(clothing);
            clothing = _mapper.Map(request, clothing);

            await _clothingRepository.UpdateAsync(clothing!);

            UpdatedClothingResponse response = _mapper.Map<UpdatedClothingResponse>(clothing);
            return response;
        }
    }
}

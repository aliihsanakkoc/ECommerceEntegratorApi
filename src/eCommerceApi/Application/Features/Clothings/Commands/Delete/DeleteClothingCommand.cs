using Application.Features.Clothings.Constants;
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

namespace Application.Features.Clothings.Commands.Delete;

public class DeleteClothingCommand : IRequest<DeletedClothingResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ClothingsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetClothings"];

    public class DeleteClothingCommandHandler : IRequestHandler<DeleteClothingCommand, DeletedClothingResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClothingRepository _clothingRepository;
        private readonly ClothingBusinessRules _clothingBusinessRules;

        public DeleteClothingCommandHandler(IMapper mapper, IClothingRepository clothingRepository,
                                         ClothingBusinessRules clothingBusinessRules)
        {
            _mapper = mapper;
            _clothingRepository = clothingRepository;
            _clothingBusinessRules = clothingBusinessRules;
        }

        public async Task<DeletedClothingResponse> Handle(DeleteClothingCommand request, CancellationToken cancellationToken)
        {
            Clothing? clothing = await _clothingRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _clothingBusinessRules.ClothingShouldExistWhenSelected(clothing);

            await _clothingRepository.DeleteAsync(clothing!);

            DeletedClothingResponse response = _mapper.Map<DeletedClothingResponse>(clothing);
            return response;
        }
    }
}
using Application.Features.VariantProducts.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.VariantProducts.Constants.VariantProductsOperationClaims;
namespace Application.Features.VariantProducts.RangeCommands;
public class AddRangeVariantProductsCommand : IRequest<Unit>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required int[] VariantIds { get; set; }
    public required int ProductId { get; set; }

    public string[] Roles => [Admin, Write, VariantProductsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetVariantProducts"];

    public class AddRangeVariantProductsCommandHandler : IRequestHandler<AddRangeVariantProductsCommand, Unit>
    {
        private readonly IVariantProductRepository _variantProductRepository;

        public AddRangeVariantProductsCommandHandler(IVariantProductRepository variantProductRepository)
        {
            _variantProductRepository = variantProductRepository;
        }

        public async Task<Unit> Handle(AddRangeVariantProductsCommand request, CancellationToken cancellationToken)
        {
            List<VariantProduct> variantProducts = new();   
            foreach(int id in request.VariantIds)
            {
                VariantProduct variantProduct = new()
                {
                    ProductId = request.ProductId,
                    VariantId = id
                };
                variantProducts.Add(variantProduct);
            }
            await _variantProductRepository.AddRangeAsync(variantProducts);
            return Unit.Value;
        }
    }
}
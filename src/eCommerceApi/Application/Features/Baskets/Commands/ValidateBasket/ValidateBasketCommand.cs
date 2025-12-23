using Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Baskets.Constants.BasketsOperationClaims;
namespace Application.Features.Baskets.Commands.ValidateBasket;

public class ValidateBasketCommand : IRequest<ValidateBasketResponse>, ISecuredRequest
{
    public decimal CartTotal { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new();

    public string[] Roles => [Admin, Read, Client];

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public List<int> VariantIds { get; set; } = new();
        public int Quantity { get; set; }
    }

    public class ValidateBasketCommandHandler : IRequestHandler<ValidateBasketCommand, ValidateBasketResponse>
    {
        private readonly IProductRepository _productRepository;

        public ValidateBasketCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ValidateBasketResponse> Handle(ValidateBasketCommand request, CancellationToken cancellationToken)
        {
            var response = new ValidateBasketResponse { IsSuccess = true };
            var productIds = request.CartItems.Select(x => x.ProductId).Distinct().ToList();

            var products = await _productRepository.GetListAsync(
                predicate: p => productIds.Contains(p.Id),
                include: p => p.Include(x => x.VariantProducts),
                cancellationToken: cancellationToken
            );

            decimal calculatedTotal = 0;
            bool hasMismatch = false;

            foreach (var item in request.CartItems)
            {
                var product = products.Items.FirstOrDefault(p => p.Id == item.ProductId);

                if (product == null)
                {
                    hasMismatch = true;
                    continue;
                }

                calculatedTotal += product.ProductPrice * item.Quantity;

                // Validate Variants Only if Product has Variants
                if (product.VariantProducts != null && product.VariantProducts.Any())
                {
                    var validVariantIds = product.VariantProducts.Select(vp => vp.VariantId).ToList();

                    // Check if UI sent variants that are NOT valid for this product
                    if (item.VariantIds.Any(vid => !validVariantIds.Contains(vid)))
                    {
                        hasMismatch = true;
                    }

                    // If Product has variants, we typically expect a selection. 
                    // But if strict requirements weren't detailed, we assume checking 'validity of sent ids' is key.
                    // However, 'varyant uyumsuzluğu' implies state mismatch.
                    // If user sends empty list but variants exist, is it a mismatch? 
                    // Let's assume Yes for consistency, but if unsure, maybe skip?
                    // User said: "Bazı productlarda variant olmayabilir, bunlarda varyant check yapma." -> Only checking the negative case.
                    // I will stick to: If you sent variants, they MUST be valid.
                    // If you sent NO variants, but product has them -> I will flag it as mismatch because price/order likely depends on it?
                    // "UI için ... mismatch nesne dön".
                    if (!item.VariantIds.Any())
                    {
                        hasMismatch = true;
                    }
                }
                // Else: Do not check variants.
            }

            if (calculatedTotal != request.CartTotal)
            {
                hasMismatch = true;
            }

            if (hasMismatch)
            {
                response.IsSuccess = false;
                foreach (var item in request.CartItems)
                {
                    var product = products.Items.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product != null)
                    {
                        response.Mismatches.Add(new ValidateBasketResponse.ProductMismatchDto
                        {
                            ProductId = product.Id,
                            ProductPrice = product.ProductPrice, // Return DB Price
                            VariantIds = product.VariantProducts?.Select(vp => vp.VariantId).ToList() ?? new List<int>() // Return Valid DB Variants
                        });
                    }
                }
            }

            response.ExpectedTotal = calculatedTotal;

            return response;
        }
    }
}

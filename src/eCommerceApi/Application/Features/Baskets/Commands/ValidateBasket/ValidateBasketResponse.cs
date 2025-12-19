using System.Collections.Generic;

namespace Application.Features.Baskets.Commands.ValidateBasket;

public class ValidateBasketResponse
{
    public bool IsSuccess { get; set; }
    public decimal ExpectedTotal { get; set; }
    public List<ProductMismatchDto> Mismatches { get; set; } = new();

    public class ProductMismatchDto
    {
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public List<int> VariantIds { get; set; } = new();
    }
}

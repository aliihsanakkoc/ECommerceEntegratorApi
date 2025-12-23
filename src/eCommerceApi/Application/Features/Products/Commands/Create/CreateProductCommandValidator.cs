using FluentValidation;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.ProductCode).NotEmpty();
        RuleFor(c => c.ProductName).NotEmpty();
        RuleFor(c => c.ProductPrice).NotEmpty();
        RuleFor(c => c.IsAddToCart).NotNull();
        RuleFor(c => c.ProductType).NotEmpty();
    }
}

using FluentValidation;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ProductCode).NotEmpty();
        RuleFor(c => c.ProductName).NotEmpty();
        RuleFor(c => c.ProductPrice).NotEmpty();
        RuleFor(c => c.IsAddToCart).NotNull();
        RuleFor(c => c.ProductType).NotEmpty();
    }
}
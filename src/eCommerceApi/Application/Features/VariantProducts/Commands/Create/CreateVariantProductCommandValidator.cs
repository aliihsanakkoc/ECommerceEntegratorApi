using FluentValidation;

namespace Application.Features.VariantProducts.Commands.Create;

public class CreateVariantProductCommandValidator : AbstractValidator<CreateVariantProductCommand>
{
    public CreateVariantProductCommandValidator()
    {
        RuleFor(c => c.VariantId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
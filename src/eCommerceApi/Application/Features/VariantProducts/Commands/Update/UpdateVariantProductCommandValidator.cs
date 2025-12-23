using FluentValidation;

namespace Application.Features.VariantProducts.Commands.Update;

public class UpdateVariantProductCommandValidator : AbstractValidator<UpdateVariantProductCommand>
{
    public UpdateVariantProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.VariantId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}

using FluentValidation;

namespace Application.Features.VariantProducts.Commands.Delete;

public class DeleteVariantProductCommandValidator : AbstractValidator<DeleteVariantProductCommand>
{
    public DeleteVariantProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
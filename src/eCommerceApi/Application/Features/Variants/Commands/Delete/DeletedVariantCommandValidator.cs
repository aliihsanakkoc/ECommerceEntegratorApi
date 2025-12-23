using FluentValidation;

namespace Application.Features.Variants.Commands.Delete;

public class DeleteVariantCommandValidator : AbstractValidator<DeleteVariantCommand>
{
    public DeleteVariantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

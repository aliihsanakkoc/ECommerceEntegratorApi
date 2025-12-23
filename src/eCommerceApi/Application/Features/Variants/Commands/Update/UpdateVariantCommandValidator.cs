using FluentValidation;

namespace Application.Features.Variants.Commands.Update;

public class UpdateVariantCommandValidator : AbstractValidator<UpdateVariantCommand>
{
    public UpdateVariantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.VariantName).NotEmpty();
        RuleFor(c => c.TopVariantName).NotEmpty();
        RuleFor(c => c.TopVariantId).NotEmpty();
    }
}

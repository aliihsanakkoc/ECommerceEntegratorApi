using FluentValidation;

namespace Application.Features.Variants.Commands.Create;

public class CreateVariantCommandValidator : AbstractValidator<CreateVariantCommand>
{
    public CreateVariantCommandValidator()
    {
        RuleFor(c => c.VariantName).NotEmpty();
        RuleFor(c => c.TopVariantName).NotEmpty();
        RuleFor(c => c.TopVariantId).NotEmpty();
    }
}
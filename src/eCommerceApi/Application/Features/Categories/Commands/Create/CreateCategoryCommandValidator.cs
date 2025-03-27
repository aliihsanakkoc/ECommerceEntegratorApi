using FluentValidation;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.SingleCategoryName).NotEmpty();
        RuleFor(c => c.IsProductCategorization).NotNull();
        RuleFor(c => c.TopCategoryName).NotEmpty();
        RuleFor(c => c.TopCategoryId).NotEmpty();
    }
}
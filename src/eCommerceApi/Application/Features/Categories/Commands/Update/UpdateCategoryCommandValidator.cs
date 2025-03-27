using FluentValidation;

namespace Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SingleCategoryName).NotEmpty();
        RuleFor(c => c.IsProductCategorization).NotNull();
        RuleFor(c => c.TopCategoryName).NotEmpty();
        RuleFor(c => c.TopCategoryId).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.CategoryProducts.Commands.Update;

public class UpdateCategoryProductCommandValidator : AbstractValidator<UpdateCategoryProductCommand>
{
    public UpdateCategoryProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}

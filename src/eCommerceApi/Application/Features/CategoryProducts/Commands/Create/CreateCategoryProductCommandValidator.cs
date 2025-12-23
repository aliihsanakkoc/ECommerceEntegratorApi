using FluentValidation;

namespace Application.Features.CategoryProducts.Commands.Create;

public class CreateCategoryProductCommandValidator : AbstractValidator<CreateCategoryProductCommand>
{
    public CreateCategoryProductCommandValidator()
    {
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}

using FluentValidation;

namespace Application.Features.CategoryProducts.Commands.Delete;

public class DeleteCategoryProductCommandValidator : AbstractValidator<DeleteCategoryProductCommand>
{
    public DeleteCategoryProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}

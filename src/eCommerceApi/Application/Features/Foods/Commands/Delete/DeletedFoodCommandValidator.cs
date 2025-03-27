using FluentValidation;

namespace Application.Features.Foods.Commands.Delete;

public class DeleteFoodCommandValidator : AbstractValidator<DeleteFoodCommand>
{
    public DeleteFoodCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
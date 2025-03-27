using FluentValidation;

namespace Application.Features.Foods.Commands.Create;

public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
{
    public CreateFoodCommandValidator()
    {
        RuleFor(c => c.StorageCondition).NotEmpty();
        RuleFor(c => c.ExpiryDate).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
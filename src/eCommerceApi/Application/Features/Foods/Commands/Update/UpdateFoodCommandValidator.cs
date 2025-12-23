using FluentValidation;

namespace Application.Features.Foods.Commands.Update;

public class UpdateFoodCommandValidator : AbstractValidator<UpdateFoodCommand>
{
    public UpdateFoodCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StorageCondition).NotEmpty();
        RuleFor(c => c.ExpiryDate).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}

using FluentValidation;

namespace Application.Features.Clothings.Commands.Update;

public class UpdateClothingCommandValidator : AbstractValidator<UpdateClothingCommand>
{
    public UpdateClothingCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MadeIn).NotEmpty();
        RuleFor(c => c.FiberComposition).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
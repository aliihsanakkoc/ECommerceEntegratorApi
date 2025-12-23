using FluentValidation;

namespace Application.Features.Clothings.Commands.Create;

public class CreateClothingCommandValidator : AbstractValidator<CreateClothingCommand>
{
    public CreateClothingCommandValidator()
    {
        RuleFor(c => c.MadeIn).NotEmpty();
        RuleFor(c => c.FiberComposition).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}

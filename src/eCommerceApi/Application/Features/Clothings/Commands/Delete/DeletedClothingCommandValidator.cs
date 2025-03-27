using FluentValidation;

namespace Application.Features.Clothings.Commands.Delete;

public class DeleteClothingCommandValidator : AbstractValidator<DeleteClothingCommand>
{
    public DeleteClothingCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.Books.Commands.Update;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Author).NotEmpty();
        RuleFor(c => c.ISBN).NotEmpty();
        RuleFor(c => c.Publisher).NotEmpty();
        RuleFor(c => c.PublishedDate).NotEmpty();
        RuleFor(c => c.Edition).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}

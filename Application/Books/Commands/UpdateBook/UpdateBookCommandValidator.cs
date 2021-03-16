using FluentValidation;

namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty();
            RuleFor(b => b.Title)
               .NotEmpty()
               .MaximumLength(100);
            RuleFor(b => b.Description)
                .NotEmpty()
                .MaximumLength(200)
                .NotEqual(b => b.Title)
                .WithMessage("The provided description should be different from the title");
            RuleFor(b => b.PageCount)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(b => b.AuthorId).NotEmpty();
            RuleFor(b => b.BookListId).NotEmpty();
        }
    }
}

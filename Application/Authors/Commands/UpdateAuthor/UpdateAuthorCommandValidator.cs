using FluentValidation;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty();
            RuleFor(a => a.FirstName)
              .NotEmpty()
              .MaximumLength(30);
            RuleFor(a => a.LastName)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}

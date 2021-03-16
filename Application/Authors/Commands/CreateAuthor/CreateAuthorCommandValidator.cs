using FluentValidation;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(a => a.LastName)
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}

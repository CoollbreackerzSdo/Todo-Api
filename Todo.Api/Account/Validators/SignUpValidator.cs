using FluentValidation;

namespace Todo.Api.Account.Validators;

public sealed class SignUpValidator : AbstractValidator<SignUpRequest>
{
    public SignUpValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MustAsync((x, _) => Task.FromResult(x.Count(char.IsNumber) >= 4)).WithMessage("The Password property must be at least more than 4 numeric characters")
            .MustAsync((x, _) => Task.FromResult(x.Count(char.IsUpper) >= 2)).WithMessage("The Password property must be at least more than 2 uppercase characters");
    }
}
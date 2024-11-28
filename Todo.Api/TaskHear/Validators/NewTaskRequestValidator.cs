using FluentValidation;

namespace Todo.Api.TaskHear.Validators;

public sealed class NewTaskRequestValidator : AbstractValidator<NewTaskRequest>
{
    public NewTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .NotEmpty();
    }
}
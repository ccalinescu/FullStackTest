using EzraTest.Api.Models;
using FluentValidation;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Task name cannot be empty.")
            .MinimumLength(3).WithMessage("Task name must be at least 3 characters long.")
            .MaximumLength(10).WithMessage("Task name cannot exceed 10 characters.");
    }
}

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Task ID must be greater than 0.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Task name cannot be empty.")
            .MinimumLength(3).WithMessage("Task name must be at least 3 characters long.")
            .MaximumLength(10).WithMessage("Task name cannot exceed 10 characters.");
    }
}

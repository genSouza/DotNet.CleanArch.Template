using FluentValidation;
namespace DotNet.CleanArch.Template.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.");

        RuleFor(x => x.Email).EmailAddress()
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Password).Length(6, 100)
            .NotEmpty().WithMessage("Password is required.")
            .WithMessage("Password must be between 6 and 100 characters long.")
            .Equal(x => x.ConfirmPassword).WithMessage("Password does not match");

    }
}

using FluentValidation;
using FluentValidation.Validators;


namespace DotNet.CleanArch.Template.Application.Features.Authentication.Commands
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("Password is required.");
        }
    }
}

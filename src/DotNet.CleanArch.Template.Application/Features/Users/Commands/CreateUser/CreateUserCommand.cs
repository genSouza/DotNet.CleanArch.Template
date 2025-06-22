using MediatR;
using DotNet.CleanArch.Template.Domain.Common.Results;
namespace DotNet.CleanArch.Template.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand: IRequest<Result<int>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;

}

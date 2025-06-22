using DotNet.CleanArch.Template.Domain.Common.Results;
using MediatR;

namespace DotNet.CleanArch.Template.Application.Features.Authentication.Commands;

public record AuthenticateCommand: IRequest<Result<string>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    
}

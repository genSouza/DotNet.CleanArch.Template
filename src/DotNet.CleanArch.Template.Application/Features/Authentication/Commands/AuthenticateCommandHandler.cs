using DotNet.CleanArch.Template.Domain.Common.Interfaces;
using DotNet.CleanArch.Template.Domain.Common.Results;
using DotNet.CleanArch.Template.Infrastructure.Services.Auth;
using MediatR;
using Microsoft.Extensions.Options;


namespace DotNet.CleanArch.Template.Application.Features.Authentication.Commands;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Result<string>>
{
    private readonly IIdentityService _identityService;
    private readonly JwtSettings _jwtSettings;

    public AuthenticateCommandHandler(IIdentityService identityService, IOptions<JwtSettings> jwtSettings)
    {
        _identityService = identityService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<string>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _identityService.AuthenticateAsync(request.Email, request.Password);

            return token.Succeeded
            ? Result<string>.Success(token.Value)
            : Result<string>.Failure(token.Errors);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"An error occurred while processing your request: {ex.Message}");
        }

    }
}

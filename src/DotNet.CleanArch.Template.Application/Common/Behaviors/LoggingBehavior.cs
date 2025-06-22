using DotNet.CleanArch.Template.Application.Features.Users.Commands.CreateUser;
using DotNet.CleanArch.Template.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace DotNet.CleanArch.Template.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[START] {RequestName} - {@RequestData}",
            typeof(TRequest).Name,
            GetLoggableRequestData(request));

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            var response = await next();
            stopwatch.Stop();

            // Log de resultado (incluindo falhas no Result)
            if (response is Result result)
            {
                var status = result.Succeeded ? "SUCCESS" : "FAILURE";
                _logger.LogInformation(
                    "[{Status}] {RequestName} em {ElapsedMs}ms - Errors: {Errors}",
                    status, typeof(TRequest).Name,
                    stopwatch.ElapsedMilliseconds,
                    result.Succeeded ? "" : string.Join(", ", result.Errors));
            }
            else
            {
                _logger.LogInformation(
                    "[SUCCESS] {RequestName} em {ElapsedMs}ms",
                    typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(
                ex, "[ERROR] {RequestName} em {ElapsedMs}ms",
                typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);
            throw;
        }

    }

    private static object GetLoggableRequestData(TRequest request)
    {
        // Filtra propriedades sensíveis (ex.: senhas)
        if (request is CreateUserCommand cmd)
            return new { cmd.Email, cmd.FirstName }; // Exclui cmd.Password

        return request; // Log completo para outros requests
    }
}

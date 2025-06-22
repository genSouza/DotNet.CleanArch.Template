﻿using DotNet.CleanArch.Template.WebApi.Models;
using System.Net;
using System.Text.Json;

namespace DotNet.CleanArch.Template.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ApiResponse<object>
        {
            Success = false,
            Errors = ["An unexpected error occurred. Please try again later."],
#if DEBUG
            // Opcional: inclui detalhes técnicos apenas em desenvolvimento
            Details = exception.Message
#endif
        };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

}


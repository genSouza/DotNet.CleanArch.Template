using DotNet.CleanArch.Template.Domain.Common.Results;
using DotNet.CleanArch.Template.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace DotNet.CleanArch.Template.WebApi.Helpers;

public static class ActionResultHelper
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.Succeeded)
            return new OkObjectResult(new ApiResponse<T>
            {
                Success = true,
                Data = result.Value
            });


        // Maps error messages to HTTP status codes
        var firstError = result.Errors.FirstOrDefault()?.ToLowerInvariant();

        return firstError switch
        {
            var err when err.Contains("not found") => new NotFoundObjectResult(
                new ApiResponse<T> { Success = false, Errors = result.Errors }),

            var err when err.Contains("cannot be null") ||
                        err.Contains("invalid") ||
                        err.Contains("required") => new BadRequestObjectResult(
                new ApiResponse<T> { Success = false, Errors = result.Errors }),

            var err when err.Contains("authentication failed") ||
                        err.Contains("unauthorized") => new UnauthorizedObjectResult(
                new ApiResponse<T> { Success = false, Errors = result.Errors }),

            var err when err.Contains("forbidden") ||
                    err.Contains("not authorized") ||
                    err.Contains("access denied") => new ForbidResult(),

            _ => new BadRequestObjectResult(
                new ApiResponse<T> { Success = false, Errors = result.Errors })
        };
    }
}

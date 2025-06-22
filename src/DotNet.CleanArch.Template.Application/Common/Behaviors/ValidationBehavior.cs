using DotNet.CleanArch.Template.Domain.Common.Results;
using FluentValidation;
using MediatR;


namespace DotNet.CleanArch.Template.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next(cancellationToken);
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Any())
        {
            var errorMessages = failures.Select(f => f.ErrorMessage).ToArray();

            if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(errorMessages);
            }
            else if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var valueType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                    .MakeGenericType(valueType)
                    .GetMethod(nameof(Result<object>.Failure), new[] { typeof(IEnumerable<string>) });

                var failureResult = failureMethod?.Invoke(null, new object[] { errorMessages });
                return (TResponse)failureResult!;
            }
        }

        return await next();
        //var context = new ValidationContext<TRequest>(request);

        //var validationResults = await Task.WhenAll(
        //    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        //var failures = validationResults
        //    .SelectMany(r => r.Errors)
        //    .Where(f => f != null)
        //    .ToList();

        //if (failures.Any())
        //{
        //    var errorMessages = failures.Select(f => f.ErrorMessage);
        //    var resultType = typeof(TResponse);

        //    if (resultType.IsGenericType)
        //    {
        //        var valueType = resultType.GetGenericArguments()[0];
        //        var failureResult = typeof(Result<>)
        //            .MakeGenericType(valueType)
        //            .GetMethod("Failure")
        //            ?.Invoke(null, [errorMessages]);

        //        return (TResponse)failureResult!;

        //    }
        //    else
        //    {
        //        var failureResult = typeof(Result)
        //            .GetMethod("Failure")
        //            ?.Invoke(null, [errorMessages]);

        //        return (TResponse)failureResult!;
        //    }
        //}


        //return await next(cancellationToken);
    }
}

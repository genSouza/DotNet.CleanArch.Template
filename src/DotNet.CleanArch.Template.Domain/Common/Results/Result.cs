

namespace DotNet.CleanArch.Template.Domain.Common.Results;
public class Result
{
    public bool Succeeded { get; }
    public string[] Errors { get; }

    protected Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    // ÚNICO método Failure (evita ambiguidade)
    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }

    // Helper para Failure com 1 erro
    public static Result Failure(string error)
    {
        return Failure(new[] { error });
    }

    // Método Success
    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool succeeded, T? value, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    // ÚNICO método Failure (compatível com reflection)
    public new static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, default, errors);
    }

    // Helper para Failure com 1 erro
    public new static Result<T> Failure(string error)
    {
        return Failure(new[] { error });
    }

    // Método Success
    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, Array.Empty<string>());
    }
}

//// Versão não genérica (para métodos sem retorno)
//public readonly struct Result
//{
//    public bool Succeeded { get; }
//    public string[] Errors { get; }

//    private Result(bool succeeded, IEnumerable<string> errors)
//    {
//        Succeeded = succeeded;
//        Errors = errors.ToArray();
//    }

//    public static Result Success() => new(true, Array.Empty<string>());
//    public static Result Failure(string error) => new(false, new[] { error });
//    public static Result Failure(IEnumerable<string> errors) => new(false, errors);

//    public void Deconstruct(out bool succeeded, out string[] errors)
//    {
//        succeeded = Succeeded;
//        errors = Errors;
//    }
//}

//// Versão genérica (para métodos com retorno)
//public readonly struct Result<T>
//{
//    public bool Succeeded { get; }
//    public T? Value { get; }
//    public string[] Errors { get; }

//    private Result(bool succeeded, T? value, IEnumerable<string> errors)
//    {
//        Succeeded = succeeded;
//        Value = value;
//        Errors = errors.ToArray();
//    }

//    public static Result<T> Success(T value) => new(true, value, Array.Empty<string>());
//    public static Result<T> Failure(string error) => new(false, default, new[] { error });
//    public static Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors);

//    public void Deconstruct(out bool succeeded, out T? value, out string[] errors)
//    {
//        succeeded = Succeeded;
//        value = Value;
//        errors = Errors;
//    }

//    // Conversão implícita de Result para Result<T>
//    public static implicit operator Result<T>(Result result)
//    {
//        return new Result<T>(result.Succeeded, default, result.Errors);
//    }
//}




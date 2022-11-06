using TransfusionAPI.Domain.Exceptions;

namespace TransfusionAPI.Application.Common.Models;


public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(string error)
    {
        return new Result(false, new List<string>() { error });
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }

    public static Result<T> Success<T>(T payload)
    {
        return new Result<T>(payload, true, Array.Empty<string>());
    }

    public static Result<T> Failure<T>(T? payload, string error)
    {
        return new Result<T>(payload, false, new List<string>() { error });
    }

    public static Result<T> Failure<T>(T? payload, IEnumerable<string> errors)
    {
        return new Result<T>(payload, false, errors);
    }
}

public class Result<T> : Result
{
    public T Payload { get; }

    internal Result(T? payload, bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        if (payload is null && succeeded)
            throw InvalidResultException.PayloadCannotBeNullableWhenSuccessfulResult();

        Payload = payload!;
    }
}

namespace PetFamily.Domain.Common;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException();

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException();

        Error = error;
        IsSuccess = isSuccess;
    }

    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);

    public static implicit operator Result(Error error) => new(false, error);
}
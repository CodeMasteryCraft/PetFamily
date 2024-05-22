namespace PetFamily.Domain.Common;

public class Result<TValue> : Result
{
    public Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    private readonly TValue _value;

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public static implicit operator Result<TValue>(TValue value) => new(value, true, Error.None);

    public static implicit operator Result<TValue>(Error error) => new(default!, false, error);
}
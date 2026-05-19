namespace Legacy.Shared.Utility;

public class Result<TValue, TError>
{
    private readonly TValue? _value;
    private readonly TError? _error;

    private Result(TValue value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }

    private Result(TError error)
    {
        IsError = true;
        _error = error;
        _value = default;
    }

    private bool IsError { get; }

    public bool IsSuccess => !IsError;

    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);
    
    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<TError, TResult> failure) => !IsError ? success(_value!) : failure(_error!);
    
    public TResult FailureResult<TResult>(Func<TError, TResult> failure) => failure(_error!);
    
    public TResult SuccessResult<TResult>(Func<TValue, TResult> success) => success(_value!);
}

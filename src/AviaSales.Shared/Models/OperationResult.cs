namespace AviaSales.Shared.Models;

public class OperationResult<TData, TError>
{
    public TData? Data { get; private set; }
    public TError? Error { get; private set; }
    public bool IsSuccess => Error == null;

    private OperationResult(TData data, TError error)
    {
        Data = data;
        Error = error;
    }

    public static OperationResult<TData, TError> Success(TData data)
    {
        return new OperationResult<TData, TError>(data, default);
    }

    public static OperationResult<TData, TError> Failure(TError error)
    {
        return new OperationResult<TData, TError>(default, error);
    }
}
namespace PracticeAuth.Models;

public class ServiceResponse<T>
{
    public bool Success { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorMessage { get; init; }
    public T? Data { get; init; }
    
    public static ServiceResponse<T> Ok(T data)
    {
        return new ServiceResponse<T>
        {
            Success = true,
            Data = data
        };
    }

    public static ServiceResponse<T> Fail(string errorCode, string errorMessage)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        };
    }
    
}
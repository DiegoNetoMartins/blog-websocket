
namespace Blog.Shared.Responses;

public class BaseResponse
{
    protected BaseResponse() { }

    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public List<ValidationErrorResponse>? Errors { get; set; }

    public static BaseResponse Success()
        => new()
        {
            IsSuccess = true
        };

    public static BaseResponse ValidationError(List<ValidationErrorResponse> errors)
        => new()
        {
            Message = "One or more validation errors occurred.",
            Errors = errors,
            IsSuccess = false
        };

    public static BaseResponse Error(string message)
        => new()
        {
            Message = message,
            IsSuccess = false
        };
}

public class BaseResponse<T> : BaseResponse
{
    protected BaseResponse() { }

    public T? Data { get; private set; }

    public static BaseResponse<T> Success(T data)
        => new()
        {
            Data = data,
            IsSuccess = true
        };

    public static new BaseResponse<T> ValidationError(List<ValidationErrorResponse> errors)
        => new()
        {
            Message = "One or more validation errors occurred.",
            Errors = errors,
            IsSuccess = false
        };

    public static new BaseResponse<T> Error(string message)
        => new()
        {
            Message = message,
            IsSuccess = false
        };
}

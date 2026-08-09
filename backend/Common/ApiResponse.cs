namespace TaskPilot.Api.Common;

public class ApiResponse<T>
{
    public ApiResponse(
        bool success,
        T? data = default,
        string? message = null,
        List<string>? errors = null)
    {
        Success = success;
        Data = data;
        Message = message ?? string.Empty;
        Errors = errors;
    }

    public bool Success { get; set; }

    public T? Data { get; set; }

    public string Message { get; set; }

    public List<string>? Errors { get; set; }
}
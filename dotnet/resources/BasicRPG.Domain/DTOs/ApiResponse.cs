namespace BasicRPG.Domain.DTOs;

public record ApiResponse(ApiResponseType ResponseType, string Message, object? Data);

public enum ApiResponseType
{
    Success,
    Fail,
    Exception
}
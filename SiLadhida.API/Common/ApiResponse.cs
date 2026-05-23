namespace SiLadhida.API.Common;

/// <summary>
/// Standard API response wrapper for consistent response format
/// </summary>
/// <typeparam name="T">The type of data contained in the response</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the operation was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// A message describing the result
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The response data (may be null for errors)
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Creates a success response with the provided data
    /// </summary>
    public static ApiResponse<T> SuccessResponse(
        T? data,
        string message = "Success")
    {
        ArgumentNullException.ThrowIfNull(message);

        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates an error response with the provided message
    /// </summary>
    public static ApiResponse<T> ErrorResponse(string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        return new ApiResponse<T>
        {
            Success = false,
            Message = message
        };
    }
}
namespace SGE.Core.Models;

/// <summary>
/// Represents a standardized structure for error responses in an application.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the error message associated with the response.
    /// This message provides details about the error that occurred.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the code that represents the specific error encountered.
    /// This code can be used for categorizing and identifying the type of error.
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the HTTP status code associated with the error response.
    /// This code indicates the type and category of the error that occurred.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the error occurred.
    /// This value is recorded in UTC format to ensure consistency across different time zones.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the unique identifier for tracing the request across different systems or components.
    /// This property is particularly useful for tracking and diagnosing issues within distributed systems.
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// Gets or sets a dictionary containing validation errors for specific fields.
    /// The key represents the field name, and the value is a list of associated validation error messages.
    /// </summary>
    public Dictionary<string, List<string>>? ValidationErrors { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ErrorResponse"/> class with the specified error details.
    /// </summary>
    /// <param name="message">The error message describing the issue.</param>
    /// <param name="errorCode">The code representing the type or category of the error.</param>
    /// <param name="statusCode">The HTTP status code associated with the error.</param>
    /// <param name="traceId">An optional trace identifier for tracking the error.</param>
    /// <returns>A new instance of <see cref="ErrorResponse"/> populated with the provided error details.</returns>
    public static ErrorResponse Create(string message, string errorCode, int statusCode, string? traceId = null)
    {
        return new ErrorResponse
        {
            Message = message,
            ErrorCode = errorCode,
            StatusCode = statusCode,
            TraceId = traceId
        };
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ErrorResponse"/> class specifically for validation errors.
    /// </summary>
    /// <param name="validationErrors">A dictionary containing validation error messages, where the key represents the field and the value is a list of error messages for that field.</param>
    /// <param name="traceId">An optional trace identifier for tracking the validation error.</param>
    /// <returns>A new instance of <see cref="ErrorResponse"/> populated with validation error details.</returns>
    public static ErrorResponse CreateValidation(Dictionary<string, List<string>> validationErrors,
        string? traceId = null)
    {
        return new ErrorResponse
        {
            Message = "Erreurs de validation",
            ErrorCode = "VALIDATION_ERROR",
            StatusCode = 400,
            ValidationErrors = validationErrors,
            TraceId = traceId
        };
    }

}
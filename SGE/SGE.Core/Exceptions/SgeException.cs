namespace SGE.Core.Exceptions;

/// <summary>
/// Represents an exception specific to the SGE domain. This exception is designed
/// to encapsulate detailed error information, including a message, an error code,
/// and an HTTP status code.
/// </summary>
public class SgeException : Exception
{
    /// <summary>
    /// Gets the error code associated with the exception.
    /// This property provides a string identifier that represents
    /// the specific error condition related to the exception.
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// Gets the HTTP status code associated with the exception.
    /// This property provides an integer value representing the HTTP
    /// response status code that corresponds to the specific error
    /// condition encapsulated by the exception.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Represents a custom exception type for the SGE domain.
    /// This exception is intended to provide additional contextual information
    /// about the error by including an error code and an associated HTTP status code.
    /// </summary>
    protected SgeException(string message, string errorCode, int statusCode)
        : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Represents a custom exception type for the SGE domain.
    /// This exception is designed to provide additional contextual information
    /// about errors, including an error code and an HTTP status code.
    /// </summary>
    protected SgeException(string message, string errorCode, int statusCode, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}

using System.Text.Json;
using SGE.Core.Exceptions;
using SGE.Core.Models;

namespace SGE.API.Middleware;

/// <summary>
/// Middleware for handling unhandled exceptions globally in the request processing pipeline.
/// Captures exceptions, logs the details, and formats a standardized error response for clients.
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    /// <summary>
    /// Represents the next middleware delegate in the HTTP request processing pipeline.
    /// Responsible for invoking the subsequent middleware components.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Provides logging functionality specifically for the <see cref="GlobalExceptionHandlingMiddleware"/> class.
    /// Used to log details of unhandled exceptions and other relevant middleware operations.
    /// </summary>
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Middleware for handling unhandled exceptions in a global manner during the HTTP request pipeline execution.
    /// It catches exceptions thrown by downstream middleware, logs them, and provides a consistent error response to clients.
    /// </summary>
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
   {
       _next = next;
       _logger = logger;
   }

    /// <summary>
    /// Processes the HTTP request within the middleware pipeline.
    /// Captures unhandled exceptions, applies logging, and ensures a consistent error response for clients.
    /// Allows the next middleware in the pipeline to execute if no exceptions are thrown.
    /// </summary>
    /// <param name="context">The current HTTP context containing information about the request.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
           await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    /// <summary>
    /// Asynchronously handles exceptions that occur during the execution of middleware and generates an appropriate error response
    /// based on the exception type. It logs the exception details and sets the HTTP response status code, content type,
    /// and error payload accordingly.
    /// </summary>
    /// <param name="context">The current HTTP context that encapsulates all HTTP-specific information about the request and response.</param>
    /// <param name="exception">The exception that needs to be handled and translated into a client-consumable error response.</param>
    /// <returns>A task representing the asynchronous operation that writes the error response to the HTTP response stream.</returns> 
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var traceId = context.TraceIdentifier;
      
        _logger.LogError(exception, "Exception non gérée capturée. TraceId: {TraceId}", traceId);

        var errorResponse = exception switch
        {
            ValidationException validationException => ErrorResponse.CreateValidation(
                validationException.Errors,
                traceId),
           
            SgeException sgeException => ErrorResponse.Create(
                sgeException.Message,
                sgeException.ErrorCode,
                sgeException.StatusCode,
                traceId),

            ArgumentNullException => ErrorResponse.Create(
                "Un paramètre requis est manquant.",
                "ARGUMENT_NULL",
                400,
                traceId),

            ArgumentException => ErrorResponse.Create(
                "Un paramètre fourni est invalide.",
                "INVALID_ARGUMENT",
                400,
                traceId),

            UnauthorizedAccessException => ErrorResponse.Create(
                "Accès non autorisé.",
                "UNAUTHORIZED",
                401,
                traceId),

            NotImplementedException => ErrorResponse.Create(
                "Fonctionnalité non implémentée.",
                "NOT_IMPLEMENTED",
                501,
                traceId),

            TimeoutException => ErrorResponse.Create(
                "L'opération a expiré.",
                "TIMEOUT",
                408,
                traceId),

            _ => ErrorResponse.Create(
                "Une erreur interne du serveur est survenue.",
                "INTERNAL_SERVER_ERROR",
                500,
                traceId)
        };

        context.Response.StatusCode = errorResponse.StatusCode;
        context.Response.ContentType = "application/json";

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, jsonOptions);
        await context.Response.WriteAsync(jsonResponse);
    }

}
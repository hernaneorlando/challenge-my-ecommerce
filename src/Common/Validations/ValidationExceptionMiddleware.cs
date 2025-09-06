using Common.APICommon;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Security.Authentication.ExtendedProtection;
using System.Text.Json;

namespace Common.Validations;

public class ValidationExceptionMiddleware
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly RequestDelegate _next;


    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var message = ex switch
            {
                UnauthorizedAccessException unauthorizedAccessException => await HandleUnauthorizedAccessExceptionAsync(context, unauthorizedAccessException),
                KeyNotFoundException _ => await HandleValidationExceptionAsync(context, new ValidationException([new ValidationFailure("Id", ex.Message)])),
                ValidationException validationException => await HandleValidationExceptionAsync(context, validationException),
                _ => await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Server error", [new ValidationErrorDetail(ex.Message, ex.StackTrace!)])
            };

            Log.Error(message);
        }
    }

    private static async Task<string> HandleUnauthorizedAccessExceptionAsync(HttpContext context, UnauthorizedAccessException exception)
    {
        var error = new ValidationErrorDetail("Unauthorized", exception.Message);
        await HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, "Authentication Failed", [error]);
        return exception.Message;
    }

    private static Task<string> HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        var errors = exception.Errors.Select(error => (ValidationErrorDetail)error);
        return HandleExceptionAsync(context, StatusCodes.Status400BadRequest, "Validation Failed", [.. errors]);
    }
    
    private static async Task<string> HandleExceptionAsync(HttpContext context, int statusCode, string message, ICollection<ValidationErrorDetail> errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ApiResponse
        {
            Success = false,
            Message = message,
            Errors = errors
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonSerializerOptions));
        return string.Join($";{Environment.CommandLine}", errors.Select(e => $"{e.Error} - {e.Detail}"));
    }
}

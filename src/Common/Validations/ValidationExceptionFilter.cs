using Common.APICommon;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Common.Validations;

public class ValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        var message = ex switch
        {
            UnauthorizedAccessException unauthorizedAccessException => HandleUnauthorizedAccessExceptionAsync(context, unauthorizedAccessException),
            KeyNotFoundException _ => HandleValidationExceptionAsync(context, new ValidationException([new ValidationFailure("Id", ex.Message)])),
            ValidationException validationException => HandleValidationExceptionAsync(context, validationException),
            _ => HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Server error", [new ValidationErrorDetail(ex.Message, ex.StackTrace!)])
        };

        context.ExceptionHandled = true;
        Log.Error(message);
    }

    private static string HandleUnauthorizedAccessExceptionAsync(ExceptionContext context, UnauthorizedAccessException exception)
    {
        var error = new ValidationErrorDetail("Unauthorized", exception.Message);
        return HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, "Authentication Failed", [error]);
    }

    private static string HandleValidationExceptionAsync(ExceptionContext context, ValidationException exception)
    {
        var errors = exception.Errors.Select(error => (ValidationErrorDetail)error);
        return HandleExceptionAsync(context, StatusCodes.Status400BadRequest, "Validation Failed", [.. errors]);
    }

    private static string HandleExceptionAsync(ExceptionContext context, int statusCode, string message, ICollection<ValidationErrorDetail> errors)
    {
        var response = new ApiResponse
        {
            Success = false,
            Message = message,
            Errors = errors
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };

        return message;
    }
}

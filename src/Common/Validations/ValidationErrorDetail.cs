using FluentValidation.Results;

namespace Common.Validations;

public class ValidationErrorDetail
{
    public string Error { get; init; } = string.Empty;
    public string Detail { get; init; } = string.Empty;

    public ValidationErrorDetail() { }

    public ValidationErrorDetail(string error, string detail)
    {
        Error = error;
        Detail = detail;
    }

    public static explicit operator ValidationErrorDetail(ValidationFailure validationFailure)
    {
        return new ValidationErrorDetail
        {
            Detail = validationFailure.ErrorMessage,
            Error = validationFailure.ErrorCode
        };
    }
}

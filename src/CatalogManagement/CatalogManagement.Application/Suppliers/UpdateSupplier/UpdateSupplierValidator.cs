using Common.DomainCommon.Validations;
using FluentValidation;

namespace CatalogManagement.Application.Suppliers.UpdateSupplier;

/// <summary>
/// Validator for UpdateSupplierCommand that defines validation rules for supplier update command.
/// </summary>
public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSupplierValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 100 characters
    /// - Registration Number: Required, must be between 5 and 50 characters
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Phone: If not empty, must match international format (+X XXXXXXXXXX)
    /// </remarks>
    public UpdateSupplierValidator()
    {
        RuleFor(supplier => supplier.Id).NotEmpty();
        RuleFor(supplier => supplier.Name).NotEmpty().Length(3, 100);
        RuleFor(supplier => supplier.RegistrationNumber).NotEmpty().Length(5, 50);
        RuleFor(supplier => supplier.Email).SetValidator(new EmailValidator());

        RuleFor(supplier => supplier.Phone)
            .SetValidator(new PhoneValidator())
            .When(supplier => !string.IsNullOrEmpty(supplier.Phone));
    }
}

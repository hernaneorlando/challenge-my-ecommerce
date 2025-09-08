using Common.DomainCommon.Validations;
using FluentValidation;

namespace CatalogManagement.Application.Suppliers.CreateSupplier;

/// <summary>
/// Validator for CreateSupplierCommand that defines validation rules for Supplier creation command.
/// </summary>
public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSupplierValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 100 characters
    /// - Registration Number: Required, must be between 5 and 50 characters
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Phone: If not empty, must match international format (+X XXXXXXXXXX)
    /// </remarks>
    public CreateSupplierValidator()
    {
        RuleFor(supplier => supplier.Name).NotEmpty().Length(3, 100);
        RuleFor(supplier => supplier.RegistrationNumber).NotEmpty().Length(5, 50);
        RuleFor(supplier => supplier.Email).SetValidator(new EmailValidator());

        RuleFor(supplier => supplier.Phone)
            .SetValidator(new PhoneValidator())
            .When(supplier => !string.IsNullOrEmpty(supplier.Phone));
    }
}

using CatalogManagement.Domain.Entities;
using Common.DomainCommon.Validation;
using FluentValidation;

namespace CatalogManagement.Domain.Validations;

public class SupplierValidator : AbstractValidator<Supplier>
{
    public SupplierValidator()
    {
        RuleFor(supplier => supplier.Name)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Supplier name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Supplier name must not exceed 100 characters.");

        RuleFor(supplier => supplier.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Registration number must not be empty.");

        RuleFor(supplier => supplier.Email)
            .SetValidator(new EmailValidator());

        When(supplier => supplier.Phone is not null, () =>
        {
            RuleFor(supplier => supplier.Phone!.Value)
                .SetValidator(new PhoneValidator());
        });
    }
}

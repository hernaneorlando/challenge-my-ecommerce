using FluentValidation;

namespace CatalogManagement.Application.Suppliers.DeleteSupplier;

/// <summary>
/// Validator for deleting a supplier
/// </summary>
public class DeleteSupplierValidator : AbstractValidator<DeleteSupplierCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteSupplierCommand
    /// </summary>
    public DeleteSupplierValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Supplier ID is required");
    }
}

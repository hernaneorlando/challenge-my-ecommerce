using FluentValidation;

namespace CatalogManagement.Application.Suppliers.GetSupplierById;

/// <summary>
/// Validator for GetSupplierByIdCommand
/// </summary>
public class GetSupplierByIdValidator : AbstractValidator<GetSupplierByIdQuery>
{
    /// <summary>
    /// Initializes validation rules for GetSupplierByIdCommand
    /// </summary>
    public GetSupplierByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Supplier ID is required");
    }
}

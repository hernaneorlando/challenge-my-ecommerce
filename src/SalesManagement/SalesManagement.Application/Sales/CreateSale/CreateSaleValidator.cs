using FluentValidation;

namespace SalesManagement.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(s => s.CartId)
            .NotNull().NotEqual(Guid.Empty);
    }
}

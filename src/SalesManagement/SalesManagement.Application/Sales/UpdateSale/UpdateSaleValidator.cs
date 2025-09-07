using FluentValidation;

namespace SalesManagement.Application.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(s => s.Id)
            .NotEqual(Guid.Empty).WithMessage("Sale ID must be a valid GUID.");

        RuleFor(s => s.Products).NotEmpty();
    }
}

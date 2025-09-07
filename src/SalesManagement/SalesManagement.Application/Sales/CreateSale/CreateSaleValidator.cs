using System;
using FluentValidation;

namespace SalesManagement.Application.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(s => s.CartId)
            .NotNull().NotEqual(Guid.Empty);
    }
}

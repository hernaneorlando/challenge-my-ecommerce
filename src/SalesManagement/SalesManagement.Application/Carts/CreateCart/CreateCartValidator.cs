using System;
using FluentValidation;

namespace SalesManagement.Application.Carts.CreateCart;

public class CreateCartValidator : AbstractValidator<CreateCartCommand>
{
    public CreateCartValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotNull().NotEqual(Guid.Empty);

        RuleFor(c => c.BranchId)
            .NotNull().NotEqual(Guid.Empty);

        RuleFor(c => c.Products).NotEmpty();
        RuleForEach(c => c.Products)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .NotNull().NotEqual(Guid.Empty);
                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage("The product quantity must be greater than 0");
            });
    }
}

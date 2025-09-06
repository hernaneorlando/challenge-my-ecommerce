using FluentValidation;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Domain.Validations;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(c => c.Customer).NotNull();
        RuleFor(c => c.Branch).NotNull();
        RuleFor(c => c.Status).NotNull();

        RuleFor(c => c.Items).NotEmpty();
        RuleForEach(c => c.Items)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .NotNull().NotEqual(Guid.Empty)
                    .WithMessage("Product must be provided to the item.");

                item.RuleFor(i => i.SupplierId)
                    .NotNull().NotEqual(Guid.Empty)
                    .WithMessage("Supplier must be provided to the item.");

                item.RuleFor(i => i.Quantity).GreaterThan(0);
                item.RuleFor(i => i.UnitPrice).GreaterThan(0);
            });

        When(c => c.CheckoutDate is not null, () =>
        {
            RuleFor(c => c.Status)
                .Must(status => status == CartStatus.CheckedOut)
                .WithMessage("Cart must be checked out to have a shipping date");
        });
    }
}

using FluentValidation;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Validations;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(i => i.Product).NotNull();
        RuleFor(i => i.Supplier).NotNull();

        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20)
            .WithMessage("It's not possible to sell above 20 identical items");

        RuleFor(i => i.UnitPrice).GreaterThan(0);
        RuleFor(i => i.TotalAmount).GreaterThan(0);

        When(item => item.Quantity < 4, () =>
        {
            RuleFor(i => i.Discount)
                .Equal(0)
                .WithMessage("Purchases below 4 items cannot have a discount");
        });

        When(item => item.Quantity >= 4 && item.Quantity < 10, () =>
        {
            RuleFor(i => i.Discount)
                .Equal(0.1M)
                .WithMessage("Purchases above 4 identical items have a 10% discount");
        });

        When(item => item.Quantity >= 10 && item.Quantity <= 20, () =>
        {
            RuleFor(i => i.Discount)
                .Equal(0.2M)
                .WithMessage("Purchases between 10 and 20 identical items have a 20% discount");
        });
    }
}

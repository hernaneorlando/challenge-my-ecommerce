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
            .SetValidator(new CartItemValidator());

        When(c => c.CheckoutDate is not null, () =>
        {
            RuleFor(c => c.Status)
                .Must(status => status == CartStatus.CheckedOut)
                .WithMessage("Cart must be checked out to have a shipping date");
        });
    }
}

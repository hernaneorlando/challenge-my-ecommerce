using FluentValidation;

namespace SalesManagement.Application.Carts.UpdateCart;

public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartValidator()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty).WithMessage("Cart ID must be a valid GUID.");

        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty).WithMessage("Customer ID must be a valid GUID.");

        When(c => c.CheckoutDate is not null, () =>
        {
            RuleFor(c => c.CheckoutDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("The cart cannot be checked out with a past date.");
        });
    }
}

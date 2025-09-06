using FluentValidation;

namespace SalesManagement.Application.Carts.DeleteCart;

/// <summary>
/// Validator for deleting a cart
/// </summary>
public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartCommand
    /// </summary>
    public DeleteCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}

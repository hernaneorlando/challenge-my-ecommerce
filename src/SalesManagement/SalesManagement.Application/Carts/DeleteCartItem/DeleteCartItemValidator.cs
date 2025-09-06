using System;
using FluentValidation;

namespace SalesManagement.Application.Carts.DeleteCartItem;

/// <summary>
/// Validator for deleting a cart item
/// </summary>
public class DeleteCartItemValidator : AbstractValidator<DeleteCartItemCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartItemCommand
    /// </summary>
    public DeleteCartItemValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(x => x.CartItemId)
            .NotEmpty()
            .WithMessage("Cart item ID is required");
    }
}

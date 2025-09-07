using SalesManagement.Application.Carts.Common;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Application.Carts.GetCartById;

public record GetCartByIdResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer's Id.
    /// Must not be null.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or Sets the branch's Id.
    /// Must not be null.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the cart's checkout date.
    /// </summary>
    public DateOnly CheckoutDate { get; set; }

    /// <summary>
    /// Gets or sets the cart's status.
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// The date and time when the branch was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time of the last update to the branch's information
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the cart items.
    /// </summary>
    public IReadOnlyCollection<CartItemResponse> Products { get; set; } = [];
}
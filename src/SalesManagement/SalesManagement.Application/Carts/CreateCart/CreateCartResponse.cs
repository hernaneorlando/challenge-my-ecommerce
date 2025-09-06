using System.Text.Json.Serialization;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Application.Carts.CreateCart;

/// <summary>
/// API response model for CreateCart operation
/// </summary>
public record CreateCartResponse
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
    /// Gets or sets the cart items.
    /// </summary>
    public IReadOnlyCollection<CreateCartItemResponse> Products { get; set; } = [];
}

public record CreateCartItemResponse
{
    /// <summary>
    /// Gets or Sets the cart item's product Id.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the cart item's supplier Id.
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    /// Gets or Sets the cart item's quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the cart item's unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the cart item's discount.
    /// </summary>
    public decimal Discount { get; set; }
}

namespace SalesManagement.Application.Carts.Common;

public record CartItemResponse
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

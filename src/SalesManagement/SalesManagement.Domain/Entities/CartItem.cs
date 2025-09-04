using Common.DomainCommon;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a Cart Item in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class CartItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the cart's product Id.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the cart's supplier Id.
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    /// Gets or sets the cart's quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the cart's unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the cart's discount.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the cart Id associated with this item.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the cart associated with this item.
    /// </summary>
    public Cart Cart { get; set; } = null!;
}

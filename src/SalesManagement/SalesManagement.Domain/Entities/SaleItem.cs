using Common.DomainCommon;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a Sale Item in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the product's information.
    /// Must not be null.
    /// </summary>
    public SaleProduct Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the supplier's information.
    /// Must not be null.
    /// </summary>
    public SaleSupplier Supplier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the sale item's quantity.
    /// Must not be null.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the sale item's unit price.
    /// Must not be null.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the sale item's discount.
    /// Must not be null.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the sale item's total amount.
    /// Must not be null.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the sale Id associated with this item.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the sale associated with this item.
    /// </summary>
    public Sale Sale { get; set; } = null!;
}

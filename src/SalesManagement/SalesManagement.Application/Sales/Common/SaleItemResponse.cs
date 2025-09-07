namespace SalesManagement.Application.Sales.Common;

public record class SaleItemResponse
{
    /// <summary>
    /// Gets or sets the product's information.
    /// </summary>
    public SaleProductResponse Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the supplier's information.
    /// Must not be null.
    /// </summary>
    public SaleSupplierResponse Supplier { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the cart's quantity.
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
}
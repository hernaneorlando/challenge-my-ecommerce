namespace SalesManagement.Domain.Common;

public interface IItemWithDiscount
{
    /// <summary>
    /// Gets or sets the item's quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the item's discount.
    /// </summary>
    public decimal Discount { get; set; }
}

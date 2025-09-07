namespace SalesManagement.Application.Sales.Common;

public record class SaleSupplierResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the supplier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the supplier's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

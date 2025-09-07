namespace SalesManagement.Application.Sales.Common;

public record SaleProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

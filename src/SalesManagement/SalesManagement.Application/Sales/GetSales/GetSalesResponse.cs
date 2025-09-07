using SalesManagement.Application.Sales.Common;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Application.Sales.GetSales;

/// <summary>
/// API response model for GetSales operation
/// </summary>
public record class GetSalesResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale's number.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// Must not be null.
    /// </summary>
    public DateTime Date { get; set; }

    // <summary>
    /// Gets or sets the branch's information.
    /// </summary>
    public SaleBranchResponse Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the total amount for each sale item.    
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the sales's status.
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the entity's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the sale items.
    /// </summary>
    public ICollection<SaleItemResponse> Products { get; set; } = [];
}

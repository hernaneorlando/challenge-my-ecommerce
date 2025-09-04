using Common.DomainCommon;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale's number.
    /// Must not be null and not exceed 100 characters.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// Must not be null.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the branch's information.
    /// Must not be null.
    /// </summary>
    public SaleBranch Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the total amount for each sale item.
    /// Must not be null.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the sales's status.
    /// Must not be null.
    /// </summary>
    public SaleEnum Status { get; set; }

    /// <summary>
    /// Gets or sets the sale item's information.
    /// Must not be null.
    /// </summary>
    public ICollection<SaleItem> Items { get; set; } = [];
}

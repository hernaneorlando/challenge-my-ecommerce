using Common.DomainCommon;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a cart in the system with.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer's information.
    /// Must not be null.
    /// </summary>
    public User Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch's information.
    /// Must not be null.
    /// </summary>
    public Branch Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the cart's checkout date.
    /// Can be null if the cart has not been checked out yet.
    /// </summary>
    public DateOnly? CheckoutDate { get; set; }

    /// <summary>
    /// Gets or sets the cart's status.
    /// Must not be null.
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the cart items.
    /// </summary>
    public ICollection<CartItem> Items { get; set; } = [];
}

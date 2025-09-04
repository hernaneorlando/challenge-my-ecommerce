using Common.DomainCommon;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a Product, as an External Identity, in the system with minimal information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleProduct : ExternalIdentity
{
    /// <summary>
    /// Gets or sets the product's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

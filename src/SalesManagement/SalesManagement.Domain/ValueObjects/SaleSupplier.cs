using Common.DomainCommon;

namespace SalesManagement.Domain.ValueObjects;

/// <summary>
/// Represents a Supplier, as an External Identity, in the system with minimal information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleSupplier : ExternalIdentity
{
    /// <summary>
    /// Gets or sets the supplier's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

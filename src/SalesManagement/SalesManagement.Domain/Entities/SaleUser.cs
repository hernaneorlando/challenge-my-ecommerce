using Common.DomainCommon;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a User, as an External Identity, in the system with minimal information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleUser : ExternalIdentity
{
    /// <summary>
    /// Gets or sets the user's full name.
    /// Must not exceed 100 characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
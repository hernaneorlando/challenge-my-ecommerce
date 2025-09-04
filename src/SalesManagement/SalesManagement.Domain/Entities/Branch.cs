using Common.DomainCommon;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a Branch, as an External Identity, in the system with minimal information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Branch : ExternalIdentity
{
    /// <summary>
    /// Gets or sets the branch's name.
    /// Must not be null or empty, and must not exceed 100 characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's code.
    /// Must not be null or empty, and must not exceed 100 characters.
    /// </summary>
    public string Code { get; set; } = string.Empty;
}

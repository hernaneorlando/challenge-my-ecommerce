namespace CatalogManagement.Application.Suppliers.CreateSupplier;

/// <summary>
/// API response model for CreateSupplier operation
/// </summary>
public record CreateSupplierResponse
{
    /// <summary>
    /// The unique identifier of the created supplier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The supplier's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The supplier's registration number
    /// </summary>
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// The supplier's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The supplier's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}

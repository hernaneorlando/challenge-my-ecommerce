namespace SalesManagement.Application.Services.DTOs;

public record SupplierDto
{
    /// <summary>
    /// The unique identifier of the supplier
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

    /// <summary>
    /// The date and time when the supplier was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time of the last update to the supplier's information
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

using CatalogManagement.Domain.Validations;
using Common.DomainCommon;
using Common.DomainCommon.ValueObjects;
using Common.Validations;

namespace CatalogManagement.Domain.Entities;

/// <summary>
/// Represents a supplier in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Supplier : BaseEntity
{
    /// <summary>
    /// Gets or sets the supplier's full name.
    /// Must not be null or empty and be at least 3 characters long and not exceed 100 characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the supplier's registration number.
    /// Must not be null or empty and is used as a unique identifier.
    /// </summary>
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the supplier's email address.
    /// Must be a valid email format and is used as a unique identifier for communication.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the supplier's phone number.
    /// If provided, must be a valid phone number format following the pattern (XX) XXXXX-XXXX.
    /// </summary>
    public PhoneNumber? Phone { get; set; }

    /// <summary>
    /// Gets or sets the supplier's branch ID.
    /// </summary>
    public Guid? BranchId { get; set; }

    /// <summary>
    /// Gets or sets the supplier's branch.
    /// </summary>
    public Branch? Branch { get; set; }

    /// <summary>
    /// Gets or sets the supplier's list of products.
    /// </summary>
    public ICollection<Product> Products { get; set; } = [];

    // <summary>
    /// Initializes a new instance of the Supplier class.
    /// </summary>
    public Supplier()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the supplier entity using the SupplierValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Name length</list>
    /// <list type="bullet">Registration number requirement</list>
    /// <list type="bullet">Email format</list>
    /// <list type="bullet">Phone requirement and number format</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SupplierValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Update the supplier
    /// </summary>
    public ValidationResultDetail Update(Supplier supplier)
    {
        Name = supplier.Name;
        RegistrationNumber = supplier.RegistrationNumber;
        Email = supplier.Email;
        Phone = supplier.Phone;
        UpdatedAt = DateTime.UtcNow;

        return Validate();
    }
}

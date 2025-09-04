using CatalogManagement.Domain.Validations;
using Common.DomainCommon;
using Common.Validations;

namespace CatalogManagement.Domain.Entities;

/// <summary>
/// Represents a branch in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Gets or sets the branch's name.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's code.
    /// Must not be null or empty.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's descriptions.
    /// Must not be null or empty.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's address.
    /// If provided, must be at least 10 characters long and not exceed 100 characters.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the branch's list of suppliers.
    /// </summary>
    public ICollection<Supplier> Suppliers { get; set; } = [];

    /// <summary>
    /// Performs validation of the branch entity using the BranchValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Username format and length</list>
    /// <list type="bullet">Email format</list>
    /// <list type="bullet">Phone number format</list>
    /// <list type="bullet">Password complexity requirements</list>
    /// <list type="bullet">Role validity</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new BranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Update the branch
    /// </summary>
    public ValidationResultDetail Update(Branch branch)
    {
        Code = branch.Code;
        Description = branch.Description;
        Address = branch.Address;
        UpdatedAt = DateTime.UtcNow;

        return Validate();
    }
}

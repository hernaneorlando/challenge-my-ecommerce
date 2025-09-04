using CatalogManagement.Domain.Entities.ValueObjects;
using CatalogManagement.Domain.Validations;
using Common.DomainCommon;
using Common.Validations;

namespace CatalogManagement.Domain.Entities;

/// <summary>
/// Represents a product in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product's title.
    /// Must not be null or empty.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's price.
    /// Must not be less than zero.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the product's description.
    /// If provided, must be at least 10 characters long and not exceed 256 characters.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the product's category.
    /// If provided, must be at least 3 characters long and not exceed 100 characters.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the product's image path.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the product's supplier ID.
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    /// Gets or sets the product's supplier.
    /// </summary>
    public Supplier Supplier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the product's rating.
    /// </summary>
    public ProductRating? Rating { get; set; } = null!;

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title requirement</list>
    /// <list type="bullet">Price value</list>
    /// <list type="bullet">Description format and length</list>
    /// <list type="bullet">Category format and length</list>
    /// <list type="bullet">SupplierId validity</list>
    /// </remarks>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Update the product
    /// </summary>
    public ValidationResultDetail Update(Product product)
    {
        Title = product.Title;
        Price = product.Price;
        Description = product.Description;
        Category = product.Category;
        Image = product.Image;
        SupplierId = product.SupplierId;
        Supplier = product.Supplier;
        Rating = product.Rating;
        UpdatedAt = DateTime.UtcNow;

        return Validate();
    }
}

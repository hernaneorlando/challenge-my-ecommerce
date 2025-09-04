namespace CatalogManagement.Application.Products.UpdateProduct;

/// <summary>
/// API response model for UpdateProduct operation
/// </summary>
public record UpdateProductResponse
{
    /// <summary>
    /// The unique identifier of the created product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product's title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The product's price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The product's description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The product's category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// The product's image path.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// The product's supplier ID.
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    /// The product's rating.
    /// </summary>
    public string? Rating { get; set; }
}

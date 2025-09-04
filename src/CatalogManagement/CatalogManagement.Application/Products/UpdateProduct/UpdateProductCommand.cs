using System.Text.Json.Serialization;
using MediatR;

namespace CatalogManagement.Application.Products.UpdateProduct;

/// <summary>
/// Command for updating a product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a product, 
/// including name, registration number, email, and phone number. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateProductResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateProductValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record UpdateProductCommand : IRequest<UpdateProductResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be updated.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product to be created.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product to be created.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product to be created.
    /// </summary>
    public string Description { get; set; } = string.Empty;

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
    /// Gets or sets the product's rating.
    /// </summary>
    public decimal? Rating { get; set; } = null!;

    /// <summary>
    /// Gets or sets the product's rating count.
    /// </summary>
    public int? RatingCount { get; set; } = null!;

    /// <summary>
    /// Gets or sets the supplier ID associated with the product.
    /// </summary>
    public Guid SupplierId { get; set; } = Guid.Empty;
}

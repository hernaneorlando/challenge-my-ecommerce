using FluentValidation;
using MediatR;

namespace CatalogManagement.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a product, 
/// including title, price, description, category, image, rating and supplier ID. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record CreateProductCommand : IRequest<CreateProductResponse>
{
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

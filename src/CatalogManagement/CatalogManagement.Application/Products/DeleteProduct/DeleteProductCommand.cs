using MediatR;

namespace CatalogManagement.Application.Products.DeleteProduct;

/// <summary>
/// Command for deleting a product
/// </summary>
public record DeleteProductCommand(Guid Id) : IRequest;
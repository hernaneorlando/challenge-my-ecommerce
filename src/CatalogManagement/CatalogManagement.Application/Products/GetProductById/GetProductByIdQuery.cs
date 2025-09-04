using MediatR;

namespace CatalogManagement.Application.Products.GetProductById;

/// <summary>
/// Command for retrieving a Product by their ID
/// </summary>
public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResponse>;

using CatalogManagement.Domain.Entities;
using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class CreateProductHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        var createdProduct = await repository.CreateAsync(product, cancellationToken);
        return mapper.Map<CreateProductResponse>(createdProduct);
    }
}

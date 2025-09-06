using CatalogManagement.Domain.Entities;
using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests
/// </summary>
public class UpdateProductHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    /// <summary>
    /// Handles the UpdateProductCommand request
    /// </summary>
    /// <param name="command">The UpdateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        var product = mapper.Map<Product>(request);
        existingProduct.Update(product);

        var updatedProduct = await repository.UpdateAsync(existingProduct, cancellationToken);
        return mapper.Map<UpdateProductResponse>(updatedProduct);
    }
}

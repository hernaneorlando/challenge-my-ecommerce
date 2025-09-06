using CatalogManagement.Application.Repositories;
using MediatR;

namespace CatalogManagement.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests
/// </summary>
public class DeleteProductHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand>
{
    /// <summary>
    /// Handles the DeleteProductCommand request
    /// </summary>
    /// <param name="request">The DeleteProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var success = await repository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");
    }
}

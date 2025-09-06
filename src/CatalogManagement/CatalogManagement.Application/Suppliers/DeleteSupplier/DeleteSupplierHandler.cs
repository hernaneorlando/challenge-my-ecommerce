using CatalogManagement.Application.Repositories;
using MediatR;

namespace CatalogManagement.Application.Suppliers.DeleteSupplier;

/// <summary>
/// Handler for processing DeleteSupplierCommand requests
/// </summary>
public class DeleteSupplierHandler(ISupplierRepository repository) : IRequestHandler<DeleteSupplierCommand>
{
    /// <summary>
    /// Handles the DeleteSupplierCommand request
    /// </summary>
    /// <param name="request">The DeleteSupplier command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var success = await repository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Supplier with ID {request.Id} not found");
    }
}

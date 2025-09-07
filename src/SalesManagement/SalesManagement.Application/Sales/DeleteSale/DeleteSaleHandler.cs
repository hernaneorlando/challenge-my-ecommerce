using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests
/// </summary>
public class DeleteSaleHandler(ISaleRepository _saleRepository) : IRequestHandler<DeleteSaleCommand>
{
    /// <summary>
    /// Handles the DeleteSaleCommand request
    /// </summary>
    /// <param name="request">The DeleteSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
    }
}

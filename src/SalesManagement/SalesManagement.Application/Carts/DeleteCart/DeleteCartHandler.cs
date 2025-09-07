using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Carts.DeleteCart;

/// <summary>
/// Handler for processing DeleteCartCommand requests
/// </summary>
public class DeleteCartHandler(ICartRepository _cartRepository) : IRequestHandler<DeleteCartCommand>
{
    /// <summary>
    /// Handles the DeleteCartCommand request
    /// </summary>
    /// <param name="request">The DeleteCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var success = await _cartRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found");
    }
}

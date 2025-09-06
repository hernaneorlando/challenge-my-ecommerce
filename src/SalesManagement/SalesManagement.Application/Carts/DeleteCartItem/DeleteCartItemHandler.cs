using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Carts.DeleteCartItem;

public class DeleteCartItemHandler(ICartRepository repository) : IRequestHandler<DeleteCartItemCommand>
{
    /// <summary>
    /// Handles the DeleteCartItemCommand request
    /// </summary>
    /// <param name="request">The DeleteCartItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var success = await repository.DeleteCartItemAsync(request.CartId, request.CartItemId, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Cart item with ID {request.CartId} not found");
    }
}

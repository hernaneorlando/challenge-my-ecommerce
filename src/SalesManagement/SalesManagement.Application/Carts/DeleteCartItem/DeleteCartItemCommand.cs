using MediatR;

namespace SalesManagement.Application.Carts.DeleteCartItem;

/// <summary>
/// Command for deleting a cart item
/// </summary>
public record DeleteCartItemCommand(Guid CartId, Guid CartItemId) : IRequest;
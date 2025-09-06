using MediatR;

namespace SalesManagement.Application.Carts.DeleteCart;

/// <summary>
/// Command for deleting a cart
/// </summary>
public record DeleteCartCommand(Guid Id) : IRequest;
using MediatR;

namespace SalesManagement.Application.Carts.GetCartById;

/// <summary>
/// Command for retrieving a Cart by their ID
/// </summary>
public record GetCartByIdQuery(Guid Id) : IRequest<GetCartByIdResponse>;

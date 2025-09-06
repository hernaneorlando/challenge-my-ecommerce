using System.Text.Json.Serialization;
using MediatR;

namespace SalesManagement.Application.Carts.CreateCart;

/// <summary>
/// Command for creating a new cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a cart, 
/// including code, description, address. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreatecartResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreatecartValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record CreateCartCommand : IRequest<CreateCartResponse>
{
    /// <summary>
    /// Gets or Sets the customer's Id.
    /// Must not be null.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or Sets the branch's Id.
    /// Must not be null.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or Sets the cart items.
    /// </summary>
    public ICollection<CreateCartItemCommand> Products { get; set; } = [];
}

public record CreateCartItemCommand
{
    /// <summary>
    /// Gets or Sets the cart's product Id.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or Sets the cart's quantity.
    /// </summary>
    public int Quantity { get; set; }
}
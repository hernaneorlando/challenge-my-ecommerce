using System.Text.Json.Serialization;
using MediatR;

namespace SalesManagement.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating a cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a cart, 
/// including customer id and checkout date. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateCartResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateCartValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record UpdateCartCommand : IRequest<UpdateCartResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to be updated.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or Sets the customer's Id.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the cart's checkout date.
    /// </summary>
    public DateOnly? CheckoutDate { get; set; }

    public ICollection<UpdateCartItemCommand> Products { get; set; } = [];
}

public record UpdateCartItemCommand
{
    /// <summary>
    /// Gets or Sets the cart's cart Id.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or Sets the cart's quantity.
    /// </summary>
    public int Quantity { get; set; }
}

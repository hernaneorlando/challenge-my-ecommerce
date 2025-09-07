using System.Text.Json.Serialization;
using MediatR;
using SalesManagement.Application.Sales.Common;

namespace SalesManagement.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating a sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a sale, 
/// including product, quantity and uni price of the sale items. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateCartResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateCartValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record UpdateSaleCommand : IRequest<UpdateSaleResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to be updated.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// </summary>
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// </summary>
    public bool? CancelSale { get; set; }

    /// <summary>
    /// Gets or sets the sale items list.
    /// </summary>
    public ICollection<SaleItemUpdateCommand> Products { get; set; } = [];
}

public record SaleItemUpdateCommand
{
    /// <summary>
    /// Gets or Sets the product's Id.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or Sets the product's quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or Sets the product's unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
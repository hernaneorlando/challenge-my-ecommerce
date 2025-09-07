using MediatR;

namespace SalesManagement.Application.Sales.DeleteSale;

/// <summary>
/// Command for deleting a cart
/// </summary>
public record DeleteSaleCommand(Guid Id) : IRequest;
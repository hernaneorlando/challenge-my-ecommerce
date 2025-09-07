using MediatR;

namespace SalesManagement.Application.Sales.GetSaleById;

/// <summary>
/// Command for retrieving a Sale by their ID
/// </summary>
public record GetSaleByIdQuery(Guid Id) : IRequest<GetSaleByIdResponse>;
using MediatR;

namespace SalesManagement.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a sale, 
/// including the unique identifier of the Cart used in the process. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record CreateSaleCommand(Guid CartId) : IRequest<CreateSaleResponse>;
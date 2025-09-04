using MediatR;

namespace CatalogManagement.Application.Suppliers.GetSupplierById;

/// <summary>
/// Command for retrieving a Supplier by their ID
/// </summary>
public record GetSupplierByIdQuery(Guid Id) : IRequest<GetSupplierByIdResponse>;
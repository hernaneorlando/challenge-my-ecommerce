using MediatR;

namespace CatalogManagement.Application.Suppliers.DeleteSupplier;

/// <summary>
/// Command for deleting a supplier
/// </summary>
public record DeleteSupplierCommand(Guid Id) : IRequest;
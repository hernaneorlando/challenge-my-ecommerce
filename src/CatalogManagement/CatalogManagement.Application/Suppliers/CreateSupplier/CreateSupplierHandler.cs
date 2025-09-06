using CatalogManagement.Domain.Entities;
using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Suppliers.CreateSupplier;

/// <summary>
/// Handler for processing CreateSupplierCommand requests
/// </summary>
public class CreateSupplierHandler(ISupplierRepository repository, IMapper mapper) : IRequestHandler<CreateSupplierCommand, CreateSupplierResponse>
{
    /// <summary>
    /// Handles the CreateSupplierCommand request
    /// </summary>
    /// <param name="command">The CreateSupplier command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created supplier details</returns>
    public async Task<CreateSupplierResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = mapper.Map<Supplier>(request);
        var createdSupplier = await repository.CreateAsync(supplier, cancellationToken);
        return mapper.Map<CreateSupplierResponse>(createdSupplier);
    }
}

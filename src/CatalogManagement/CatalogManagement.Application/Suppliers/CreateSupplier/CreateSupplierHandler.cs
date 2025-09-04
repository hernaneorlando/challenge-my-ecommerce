using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Repositories;
using AutoMapper;
using MediatR;

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

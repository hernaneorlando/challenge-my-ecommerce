using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace CatalogManagement.Application.Suppliers.UpdateSupplier;

/// <summary>
/// Handler for processing UpdateSupplierCommand requests
/// </summary>
public class UpdateSupplierHandler(ISupplierRepository repository, IMapper mapper) : IRequestHandler<UpdateSupplierCommand, UpdateSupplierResponse>
{
    /// <summary>
    /// Handles the UpdateSupplierCommand request
    /// </summary>
    /// <param name="command">The UpdateSupplier command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated supplier details</returns>
    public async Task<UpdateSupplierResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var existingSupplier = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Supplier with ID {request.Id} not found");
            
        var supplier = mapper.Map<Supplier>(request);
        existingSupplier.Update(supplier);

        var updatedSupplier = await repository.UpdateAsync(existingSupplier, cancellationToken);
        return mapper.Map<UpdateSupplierResponse>(updatedSupplier);
    }
}

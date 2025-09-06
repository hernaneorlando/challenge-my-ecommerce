using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Suppliers.GetSupplierById;

/// <summary>
/// Handler for processing GetSupplierByIdCommand requests
/// </summary>
public class GetSupplierByIdHandler(ISupplierRepository repository, IMapper mapper) : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdResponse>
{
    /// <summary>
    /// Handles the GetSupplierByIdCommand request
    /// </summary>
    /// <param name="request">The GetSupplierById command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The supplier details if found</returns>
    public async Task<GetSupplierByIdResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Supplier with ID {request.Id} not found");

        return mapper.Map<GetSupplierByIdResponse>(supplier);
    }
}

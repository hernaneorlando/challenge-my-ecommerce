using Common.APICommon;
using Common.ORMCommon;
using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Suppliers.GetSuppliers;

/// <summary>
/// Handler for processing GetSuppliersQuery requests
/// </summary>
public class GetSuppliersHandler(ISupplierRepository repository, IMapper mapper) : IRequestHandler<GetSuppliersQuery, PaginatedResponse<GetSuppliersResponse>>
{
    /// <summary>
    /// Handles the GetSuppliersQuery request
    /// </summary>
    /// <param name="query">The GetSuppliers query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The suppliers paginated, sorted and filtered if found</returns>
    public async Task<PaginatedResponse<GetSuppliersResponse>> Handle(GetSuppliersQuery query, CancellationToken cancellationToken)
    {
        var request = mapper.Map<PaginatedRequest>(query);
        var result = await repository.GetAll(request, cancellationToken);

        return new PaginatedResponse<GetSuppliersResponse>
        {
            Data = result.Select(mapper.Map<GetSuppliersResponse>),
            CurrentPage = request.PageNumber,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            Success = true,
            Message = "Suppliers retrieved successfully"
        };
    }
}

using Common.APICommon;
using Common.ORMCommon;
using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Branches.GetBranches;

/// <summary>
/// Handler for processing GetBranchesQuery requests
/// </summary>
public class GetBranchesHandler(ISupplierRepository repository, IMapper mapper) : IRequestHandler<GetBranchesQuery, PaginatedResponse<GetBranchesResponse>>
{
    /// <summary>
    /// Handles the GetBranchesQuery request
    /// </summary>
    /// <param name="query">The GetBranches query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branches paginated, sorted and filtered if found</returns>
    public async Task<PaginatedResponse<GetBranchesResponse>> Handle(GetBranchesQuery query, CancellationToken cancellationToken)
    {
        var request = mapper.Map<PaginatedRequest>(query);
        var result = await repository.GetAll(request, cancellationToken);

        return new PaginatedResponse<GetBranchesResponse>
        {
            Data = result.Select(mapper.Map<GetBranchesResponse>),
            CurrentPage = request.PageNumber,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            Success = true,
            Message = "Branches retrieved successfully"
        };
    }
}

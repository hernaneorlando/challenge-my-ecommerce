using AutoMapper;
using Common.APICommon;
using Common.ORMCommon;
using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Carts.GetCarts;

/// <summary>
/// Handler for processing GetCartsQuery requests
/// </summary>
public class GetCartsHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<GetCartsQuery, PaginatedResponse<GetCartsResponse>>
{
    /// <summary>
    /// Handles the GetCartsQuery request
    /// </summary>
    /// <param name="query">The GetCarts query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The carts paginated, sorted and filtered if found</returns>
    public async Task<PaginatedResponse<GetCartsResponse>> Handle(GetCartsQuery query, CancellationToken cancellationToken)
    {
        var request = mapper.Map<PaginatedRequest>(query);
        var result = await repository.GetAll(request, cancellationToken);

        return new PaginatedResponse<GetCartsResponse>
        {
            Data = result.Select(mapper.Map<GetCartsResponse>),
            CurrentPage = request.PageNumber,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            Success = true,
            Message = "Carts retrieved successfully"
        };
    }
}

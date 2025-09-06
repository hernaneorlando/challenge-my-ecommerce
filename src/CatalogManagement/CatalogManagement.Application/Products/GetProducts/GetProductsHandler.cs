using Common.APICommon;
using Common.ORMCommon;
using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Products.GetProducts;

/// <summary>
/// Handler for processing GetProductsQuery requests
/// </summary>
public class GetProductsHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductsQuery, PaginatedResponse<GetProductsResponse>>
{
    /// <summary>
    /// Handles the GetProductsQuery request
    /// </summary>
    /// <param name="query">The GetProducts query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The products paginated, sorted and filtered if found</returns>
    public async Task<PaginatedResponse<GetProductsResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var request = mapper.Map<PaginatedRequest>(query);
        var result = await repository.GetAll(request, cancellationToken);

        return new PaginatedResponse<GetProductsResponse>
        {
            Data = result.Select(mapper.Map<GetProductsResponse>),
            CurrentPage = request.PageNumber,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            Success = true,
            Message = "Products retrieved successfully"
        };
    }
}

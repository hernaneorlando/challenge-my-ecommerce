using AutoMapper;
using Common.APICommon;
using Common.ORMCommon;
using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Sales.GetSales;

/// <summary>
/// Handler for processing GetSalesQuery requests
/// </summary>
public class GetSalesHandler(ISaleRepository _saleRepository, IMapper _mapper) : IRequestHandler<GetSalesQuery, PaginatedResponse<GetSalesResponse>>
{
    /// <summary>
    /// Handles the GetSalesQuery request
    /// </summary>
    /// <param name="query">The GetSales query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sales paginated, sorted and filtered if found</returns>
    public async Task<PaginatedResponse<GetSalesResponse>> Handle(GetSalesQuery query, CancellationToken cancellationToken)
    {
        var request = _mapper.Map<PaginatedRequest>(query);
        var result = await _saleRepository.GetAll(request, cancellationToken);

        return new PaginatedResponse<GetSalesResponse>
        {
            Data = result.Select(_mapper.Map<GetSalesResponse>),
            CurrentPage = request.PageNumber,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            Success = true,
            Message = "Sales retrieved successfully"
        };
    }
}

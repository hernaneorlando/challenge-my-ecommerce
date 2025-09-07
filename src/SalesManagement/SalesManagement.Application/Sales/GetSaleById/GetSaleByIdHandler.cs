using AutoMapper;
using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Sales.GetSaleById;

/// <summary>
/// Handler for processing GetSaleByIdQuery requests
/// </summary>
public class GetSaleByIdHandler(ISaleRepository _saleRepository, IMapper _mapper) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResponse>
{
    /// <summary>
    /// Handles the GetSaleByIdQuery request
    /// </summary>
    /// <param name="request">The GetSaleById command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    public async Task<GetSaleByIdResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        return _mapper.Map<GetSaleByIdResponse>(sale);
    }
}

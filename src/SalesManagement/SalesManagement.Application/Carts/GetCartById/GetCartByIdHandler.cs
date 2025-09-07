using AutoMapper;
using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Carts.GetCartById;

/// <summary>
/// Handler for processing GetCartByIdQuery requests
/// </summary>
public class GetCartByIdHandler(ICartRepository _cartRepository, IMapper _mapper) : IRequestHandler<GetCartByIdQuery, GetCartByIdResponse>
{
    /// <summary>
    /// Handles the GetCartByIdQuery request
    /// </summary>
    /// <param name="request">The GetCartById command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    public async Task<GetCartByIdResponse> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

        return _mapper.Map<GetCartByIdResponse>(cart);
    }
}

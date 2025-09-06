using AutoMapper;
using MediatR;
using SalesManagement.Application.Repositories;

namespace SalesManagement.Application.Carts.GetCartById;

/// <summary>
/// Handler for processing GetCartByIdCommand requests
/// </summary>
public class GetCartByIdHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<GetCartByIdQuery, GetCartByIdResponse>
{
    /// <summary>
    /// Handles the GetCartByIdCommand request
    /// </summary>
    /// <param name="request">The GetCartById command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    public async Task<GetCartByIdResponse> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

        return mapper.Map<GetCartByIdResponse>(cart);
    }
}

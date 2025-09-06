using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Products.GetProductById;

/// <summary>
/// Handler for processing GetProductByIdCommand requests
/// </summary>
public class GetProductByIdHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    /// <summary>
    /// Handles the GetProductByIdCommand request
    /// </summary>
    /// <param name="request">The GetProductById command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        return mapper.Map<GetProductByIdResponse>(product);
    }
}
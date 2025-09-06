using AutoMapper;
using MediatR;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Application.Branches.GetBranchById;

/// <summary>
/// Handler for processing GetBranchByIdCommand requests
/// </summary>
public class GetBranchByIdHandler(IBranchRepository repository, IMapper mapper) : IRequestHandler<GetBranchByIdQuery, GetBranchByIdResponse>
{
    /// <summary>
    /// Handles the GetBranchByIdCommand request
    /// </summary>
    /// <param name="request">The GetBranchById command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch details if found</returns>
    public async Task<GetBranchByIdResponse> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Branch with ID {request.Id} not found");

        return mapper.Map<GetBranchByIdResponse>(branch);
    }
}

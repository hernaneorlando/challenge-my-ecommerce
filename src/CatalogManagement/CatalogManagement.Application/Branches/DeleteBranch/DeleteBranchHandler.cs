using CatalogManagement.Domain.Repositories;
using MediatR;

namespace CatalogManagement.Application.Branches.DeleteBranch;

/// <summary>
/// Handler for processing DeleteBranchCommand requests
/// </summary>
public class DeleteBranchHandler(IBranchRepository repository) : IRequestHandler<DeleteBranchCommand>
{
    /// <summary>
    /// Handles the DeleteBranchCommand request
    /// </summary>
    /// <param name="request">The DeleteBranch command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var success = await repository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Branch with ID {request.Id} not found");
    }
}

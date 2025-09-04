using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace CatalogManagement.Application.Branches.UpdateBranch;

/// <summary>
/// Handler for processing UpdateBranchCommand requests
/// </summary>
public class UpdateBranchHandler(IBranchRepository repository, IMapper mapper) : IRequestHandler<UpdateBranchCommand, UpdateBranchResponse>
{
    /// <summary>
    /// Handles the UpdateBranchCommand request
    /// </summary>
    /// <param name="command">The UpdateBranch command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated branch details</returns>
    public async Task<UpdateBranchResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var existingBranch = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Branch with ID {request.Id} not found");

        var branch = mapper.Map<Branch>(request);
        existingBranch.Update(branch);

        var updatedBranch = await repository.UpdateAsync(existingBranch, cancellationToken);
        return mapper.Map<UpdateBranchResponse>(updatedBranch);
    }
}

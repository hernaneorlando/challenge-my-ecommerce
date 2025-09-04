using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace CatalogManagement.Application.Branches.CreateBranch;

/// <summary>
/// Handler for processing CreateBranchCommand requests
/// </summary>
public class CreateBranchHandler(IBranchRepository repository, IMapper mapper) : IRequestHandler<CreateBranchCommand, CreateBranchResponse>
{
    /// <summary>
    /// Handles the CreateBranchCommand request
    /// </summary>
    /// <param name="command">The CreateBranch command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch details</returns>
    public async Task<CreateBranchResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = mapper.Map<Branch>(request);
        var createdBranch = await repository.CreateAsync(branch, cancellationToken);
        return mapper.Map<CreateBranchResponse>(createdBranch);
    }
}

using MediatR;

namespace CatalogManagement.Application.Branches.DeleteBranch;

/// <summary>
/// Command for deleting a branch
/// </summary>
public record class DeleteBranchCommand(Guid Id) : IRequest;
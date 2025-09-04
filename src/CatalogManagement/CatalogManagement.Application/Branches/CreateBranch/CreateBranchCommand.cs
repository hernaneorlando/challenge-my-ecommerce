using MediatR;

namespace CatalogManagement.Application.Branches.CreateBranch;

/// <summary>
/// Command for creating a new branch.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a branch, 
/// including code, description, address. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateBranchResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateBranchValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record CreateBranchCommand : IRequest<CreateBranchResponse>
{
    /// <summary>
    /// Gets or sets the branch's code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's descriptions.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's address.
    /// </summary>
    public string? Address { get; set; }
}

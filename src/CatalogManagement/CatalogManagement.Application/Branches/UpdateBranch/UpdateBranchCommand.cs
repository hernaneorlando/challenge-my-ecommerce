using System.Text.Json.Serialization;
using MediatR;

namespace CatalogManagement.Application.Branches.UpdateBranch;

/// <summary>
/// Command for updating a branch.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a branch, 
/// including code, description, address. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateBranchResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateBranchValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record UpdateBranchCommand : IRequest<UpdateBranchResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the branch to be updated.
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

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

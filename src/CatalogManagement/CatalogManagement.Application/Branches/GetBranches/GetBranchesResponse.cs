namespace CatalogManagement.Application.Branches.GetBranches;

/// <summary>
/// API response model for GetBranches operation
/// </summary>
public record GetBranchesResponse
{
    /// <summary>
    /// The unique identifier of the created branch
    /// </summary>
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

    /// <summary>
    /// The date and time when the branch was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time of the last update to the branch's information
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

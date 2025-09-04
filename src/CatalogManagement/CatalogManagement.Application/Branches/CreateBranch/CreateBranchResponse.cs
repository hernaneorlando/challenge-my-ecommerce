namespace CatalogManagement.Application.Branches.CreateBranch;

/// <summary>
/// API response model for CreateBranch operation
/// </summary>
public record class CreateBranchResponse
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
}

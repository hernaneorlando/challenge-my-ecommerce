namespace SalesManagement.Application.Sales.Common;

public record class SaleBranchResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the branch.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the branch's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's code.
    /// </summary>
    public string Code { get; set; } = string.Empty;
}

using Common.APICommon;

namespace CatalogManagement.Application.Branches.GetBranches;

/// <summary>
/// Query for retrieving Branches
/// </summary>
public record GetBranchesQuery : BasePagedQuery<GetBranchesResponse>;
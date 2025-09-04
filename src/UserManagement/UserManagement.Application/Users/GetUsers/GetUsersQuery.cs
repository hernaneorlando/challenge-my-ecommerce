using Common.APICommon;

namespace UserManagement.Application.Users.GetUsers;

/// <summary>
/// Query for retrieving Users
/// </summary>
public record GetUsersQuery : BasePagedQuery<GetUsersResponse>;
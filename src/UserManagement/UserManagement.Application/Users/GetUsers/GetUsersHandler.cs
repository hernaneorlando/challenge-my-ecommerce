using Common.APICommon;
using Common.ORMCommon;
using AutoMapper;
using MediatR;
using UserManagement.Application.Repositories;

namespace UserManagement.Application.Users.GetUsers;

/// <summary>
/// Handler for processing GetUsersQuery requests
/// </summary>
public class GetUsersHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetUsersQuery, PaginatedResponse<GetUsersResponse>>
{
    /// <summary>
    /// Handles the GetUsersQuery request
    /// </summary>
    /// <param name="query">The GetUsers query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The users paginated, sorted and filtered if found</returns>
    public async Task<PaginatedResponse<GetUsersResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var request = mapper.Map<PaginatedRequest>(query);
        var result = await repository.GetAll(request, cancellationToken);

        return new PaginatedResponse<GetUsersResponse>
        {
            Data = result.Select(mapper.Map<GetUsersResponse>),
            CurrentPage = request.PageNumber,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            Success = true,
            Message = "Users retrieved successfully"
        };
    }
}

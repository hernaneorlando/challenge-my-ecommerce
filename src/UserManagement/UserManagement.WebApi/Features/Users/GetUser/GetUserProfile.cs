using UserManagement.Application.Users.GetUserById;
using AutoMapper;

namespace UserManagement.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<Guid, GetUserQuery>()
            .ConstructUsing(id => new GetUserQuery(id));
        CreateMap<GetUserResult, GetUserResponse>();
    }
}

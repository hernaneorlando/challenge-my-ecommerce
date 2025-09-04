using Common.ORMCommon;
using UserManagement.Domain.Entities;
using AutoMapper;

namespace UserManagement.Application.Users.GetUsers;

public class GetUsersProfile : Profile
{
    public GetUsersProfile()
    {
        CreateMap<User, GetUsersResponse>();
        CreateMap<GetUsersQuery, PaginatedRequest>();
    }
}

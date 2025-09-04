using UserManagement.Application.Users.CreateUser;
using UserManagement.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace UserManagement.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}
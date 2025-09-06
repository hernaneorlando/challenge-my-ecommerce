using SalesManagement.Application.Services.DTOs;

namespace SalesManagement.Application.Services;

public interface IUserService
{
    UserDto GetAuthenticatedUser();
}

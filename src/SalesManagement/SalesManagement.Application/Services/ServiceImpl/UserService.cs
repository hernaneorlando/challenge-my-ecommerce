using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SalesManagement.Application.Services.DTOs;

namespace SalesManagement.Application.Services.ServiceImpl;

public class UserService(IHttpContextAccessor _httpContextAccessor) : IUserService
{
    public UserDto GetAuthenticatedUser()
    {
        var user = _httpContextAccessor.HttpContext!.User;
        return new UserDto
        {
            Id = Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId) ? userId : Guid.Empty,
            Name = user.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
            UserName = user.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
            Role = user.FindFirstValue(ClaimTypes.Role) ?? string.Empty
        };
    }
}

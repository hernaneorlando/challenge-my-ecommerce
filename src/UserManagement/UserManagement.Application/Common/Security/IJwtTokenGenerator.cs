using UserManagement.Domain.Entities;

namespace UserManagement.Application.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IUser user);
    }
}

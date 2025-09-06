using System.Linq.Expressions;
using Common.ORMCommon;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public Expression<Func<User, bool>> Predicate { get; private set; }


    public ActiveUserSpecification(string email)
    {
        Predicate = user =>
            user.Status == UserStatus.Active && user.Email == email;
    }
}

using System.Linq.Expressions;

namespace Common.ORMCommon;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Predicate { get; }
}

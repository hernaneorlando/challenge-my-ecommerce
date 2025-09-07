using System.Linq.Expressions;

namespace Common.ORMCommon;

/// <summary>
/// Repository interface for entities pagination, sorting, and filtering operations
/// </summary>
public interface IBaseRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Retrieves the list of entities paginated, sorted and filtered
    /// </summary>
    /// <param name="request">The query request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entities paginated, sorted and filtered if found</returns>
    Task<PaginatedList<TEntity>> GetAll(PaginatedRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an entityby a pre-defined constraint
    /// </summary>
    /// <param name="specification">The constraint to find the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<TEntity> Find(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an entityby a pre-defined constraint
    /// </summary>
    /// <param name="specification">The constraint to find the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<TEntity> Find<TProperty>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default, params Expression<Func<TEntity, TProperty>>[] includes);
}

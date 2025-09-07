using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Common.ORMCommon;

/// <summary>
/// Implementation of IPaginatedRepository using Entity Framework Core
/// </summary>
public abstract class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Retrieves the list of entities paginated, sorted and filtered
    /// </summary>
    /// <param name="request">The query request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entities paginated, sorted and filtered if found</returns>
    public async Task<PaginatedList<TEntity>> GetAll(PaginatedRequest request, CancellationToken cancellationToken = default)
    {
        var query = context.Set<TEntity>()
            .AsQueryable()
            .ApplyFilters(request.Filters);

        if (!string.IsNullOrEmpty(request.OrderBy))
            query = query.ApplyOrder(request.OrderBy);

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TEntity>(items, total, request.PageNumber, request.PageSize);
    }

    /// <summary>
    /// Retrieves an entityby a pre-defined constraint
    /// </summary>
    /// <param name="specification">The constraint to find the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    public async Task<TEntity> Find(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        if (specification?.Predicate is null)
            throw new ArgumentNullException("Specification must not be null and contain a valida predicate.");

        var result = await context.Set<TEntity>()
            .FirstOrDefaultAsync(specification?.Predicate!, cancellationToken);

        return result!;
    }

    /// <summary>
    /// Retrieves an entityby a pre-defined constraint
    /// </summary>
    /// <param name="specification">The constraint to find the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    public async Task<TEntity> Find<TProperty>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default, params Expression<Func<TEntity, TProperty>>[] includes)
    {
        if (specification?.Predicate is null)
            throw new ArgumentNullException("Specification must not be null and contain a valida predicate.");

        IQueryable<TEntity> query = context.Set<TEntity>();
        if (includes.Length != 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        var result = await query.FirstOrDefaultAsync(specification?.Predicate!, cancellationToken);

        return result!;
    }
}

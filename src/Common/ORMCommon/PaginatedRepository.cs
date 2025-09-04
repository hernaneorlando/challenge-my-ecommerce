using Microsoft.EntityFrameworkCore;

namespace Common.ORMCommon;

/// <summary>
/// Implementation of IPaginatedRepository using Entity Framework Core
/// </summary>
public abstract class PaginatedRepository<TEntity>(DbContext context) : IPaginatedRepository<TEntity>
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
}

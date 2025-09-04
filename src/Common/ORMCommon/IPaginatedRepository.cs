namespace Common.ORMCommon;

/// <summary>
/// Repository interface for entities pagination, sorting, and filtering operations
/// </summary>
public interface IPaginatedRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Retrieves the list of entities paginated, sorted and filtered
    /// </summary>
    /// <param name="request">The query request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entities paginated, sorted and filtered if found</returns>
    Task<PaginatedList<TEntity>> GetAll(PaginatedRequest request, CancellationToken cancellationToken = default);
}

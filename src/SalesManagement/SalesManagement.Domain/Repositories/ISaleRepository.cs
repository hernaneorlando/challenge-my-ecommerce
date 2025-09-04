using Common.ORMCommon;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository : IPaginatedRepository<Sale>
{
    /// <summary>
    /// Creates a new Sale in the repository
    /// </summary>
    /// <param name="Sale">The Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale</returns>
    Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing Sale in the repository
    /// </summary>
    /// <param name="Sale">The Sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the update was successful, false otherwise</returns>
    Task<Sale> UpdateAsync(Sale Sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a Sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the deletion was successful, false otherwise</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

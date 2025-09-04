using CatalogManagement.Domain.Entities;
using Common.ORMCommon;

namespace CatalogManagement.Domain.Repositories;

/// <summary>
/// Repository interface for Supplier entity operations
/// </summary>
public interface ISupplierRepository : IPaginatedRepository<Supplier>
{
    /// <summary>
    /// Creates a new supplier in the repository
    /// </summary>
    /// <param name="supplier">The supplier to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created supplier</returns>
    Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a supplier by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the supplier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The supplier if found, null otherwise</returns>
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing supplier in the repository
    /// </summary>
    /// <param name="supplier">The supplier to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the update was successful, false otherwise</returns>
    Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a supplier from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the supplier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the deletion was successful, false otherwise</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

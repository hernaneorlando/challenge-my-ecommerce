using Common.ORMCommon;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Repositories;

/// <summary>
/// Repository interface for Cart entity operations
/// </summary>
public interface ICartRepository : IPaginatedRepository<Cart>
{
    /// <summary>
    /// Creates a new Cart in the repository
    /// </summary>
    /// <param name="Cart">The Cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Cart</returns>
    Task<Cart> CreateAsync(Cart Cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart if found, null otherwise</returns>
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing Cart in the repository
    /// </summary>
    /// <param name="Cart">The Cart to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the update was successful, false otherwise</returns>
    Task<Cart> UpdateAsync(Cart Cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a Cart from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the deletion was successful, false otherwise</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

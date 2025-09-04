using Common.ORMCommon;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Repositories;

namespace SalesManagement.ORM.Repositories;

/// <summary>
/// Implementation of ICartRepository using EF Core
/// </summary>
public class CartRepository : PaginatedRepository<Cart>, ICartRepository
{
private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of CartRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CartRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Cart in the database.
    /// </summary>
    /// <param name="Cart">The Cart to be created.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public async Task<Cart> CreateAsync(Cart Cart, CancellationToken cancellationToken = default)
    {
        var result = await _context.Carts.AddAsync(Cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Retrieves a Cart by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Cart if found, null otherwise</returns>
    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing Cart in the database
    /// </summary>
    /// <param name="Cart">The Cart to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Cart was updated, false if not</returns>
    public async Task<Cart> UpdateAsync(Cart Cart, CancellationToken cancellationToken = default)
    {
        var result = _context.Carts.Update(Cart);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Deletes a Cart from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Cart was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Cart = await GetByIdAsync(id, cancellationToken);
        if (Cart == null)
            return false;

        _context.Carts.Remove(Cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

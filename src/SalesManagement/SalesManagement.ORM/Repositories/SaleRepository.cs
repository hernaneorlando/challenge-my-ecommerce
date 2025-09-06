using Common.ORMCommon;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Application.Repositories;
using SalesManagement.Domain.Entities;

namespace SalesManagement.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using EF Core
/// </summary>
public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Sale in the database.
    /// </summary>
    /// <param name="Sale">The Sale to be created.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public async Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default)
    {
        var result = await _context.Sales.AddAsync(Sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Retrieves a Sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing Sale in the database
    /// </summary>
    /// <param name="Sale">The Sale to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Sale was updated, false if not</returns>
    public async Task<Sale> UpdateAsync(Sale Sale, CancellationToken cancellationToken = default)
    {
        var result = _context.Sales.Update(Sale);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Deletes a Sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Sale = await GetByIdAsync(id, cancellationToken);
        if (Sale == null)
            return false;

        _context.Sales.Remove(Sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

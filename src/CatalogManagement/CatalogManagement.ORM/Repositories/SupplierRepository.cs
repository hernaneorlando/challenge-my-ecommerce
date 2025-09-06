using CatalogManagement.Application.Repositories;
using CatalogManagement.Domain.Entities;
using Common.ORMCommon;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.ORM.Repositories;

/// <summary>
/// Implementation of ISupplierRepository using EF Core
/// </summary>
public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SupplierRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SupplierRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new supplier in the database.
    /// </summary>
    /// <param name="supplier">The supplier to be created.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public async Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        var result = await _context.Suppliers.AddAsync(supplier, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Retrieves a supplier by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the supplier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The supplier if found, null otherwise</returns>
    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Suppliers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing supplier in the database
    /// </summary>
    /// <param name="supplier">The supplier to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the supplier was updated, false if not</returns>
    public async Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        var result = _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Deletes a supplier from the database
    /// </summary>
    /// <param name="id">The unique identifier of the supplier to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the supplier was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var supplier = await GetByIdAsync(id, cancellationToken);
        if (supplier == null)
            return false;

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

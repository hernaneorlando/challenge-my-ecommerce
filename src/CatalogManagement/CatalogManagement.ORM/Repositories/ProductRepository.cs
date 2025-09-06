using CatalogManagement.Application.Repositories;
using CatalogManagement.Domain.Entities;
using Common.ORMCommon;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using EF Core
/// </summary>
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product in the database.
    /// </summary>
    /// <param name="product">The product to be created.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var result = await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Retrieves a product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing product in the database
    /// </summary>
    /// <param name="product">The product to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was updated, false if not</returns>
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var result = _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Deletes a product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

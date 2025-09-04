using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.Repositories;
using Common.ORMCommon;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.ORM.Repositories;

/// <summary>
/// Implementation of IBranchRepository using EF Core
/// </summary>
public class BranchRepository : PaginatedRepository<Branch>, IBranchRepository
{
private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of BranchRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BranchRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new branch in the database.
    /// </summary>
    /// <param name="branch">The branch to be created.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        var result = await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Retrieves a branch by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing branch in the database
    /// </summary>
    /// <param name="branch">The branch to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was updated, false if not</returns>
    public async Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        var result = _context.Branches.Update(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Deletes a branch from the database
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

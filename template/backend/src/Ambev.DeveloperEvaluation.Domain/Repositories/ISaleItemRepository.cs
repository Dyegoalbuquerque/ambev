using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleItemRepository
{

    Task<List<SaleItem?>> GetBySaleIdAsync(Guid saleId);
    
    /// <summary>
    /// Deletes a Sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

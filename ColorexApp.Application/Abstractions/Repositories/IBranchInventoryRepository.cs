using ColorexApp.Domain.Entities;

namespace ColorexApp.Application.Abstractions.Repositories;

public interface IBranchInventoryRepository
{
    Task<BranchInventory?> GetByBranchAndVariantAsync(Guid branchId, Guid variantId, CancellationToken ct = default);
    Task AddAsync(BranchInventory inventory, CancellationToken ct = default);
    void Update(BranchInventory inventory);
}

using ColorexApp.Domain.Entities;

namespace ColorexApp.Application.Abstractions.Repositories;

public interface IInventoryTransferRepository
{
    Task<InventoryTransfer?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<InventoryTransfer>> GetAllAsync(CancellationToken ct = default);
    Task<List<InventoryTransfer>> GetByDestinationBranchAsync(Guid branchId, CancellationToken ct = default);
    Task AddAsync(InventoryTransfer transfer, CancellationToken ct = default);
    void Update(InventoryTransfer transfer);
}

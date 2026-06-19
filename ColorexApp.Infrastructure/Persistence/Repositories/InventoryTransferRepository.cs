using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class InventoryTransferRepository(ColorexDbContext context) : IInventoryTransferRepository
{
    public async Task<InventoryTransfer?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.InventoryTransfers
            .Include(t => t.OriginBranch)
            .Include(t => t.DestinationBranch)
            .Include(t => t.ProductVariant).ThenInclude(v => v.Product)
            .FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<List<InventoryTransfer>> GetAllAsync(CancellationToken ct = default)
        => await context.InventoryTransfers
            .Include(t => t.OriginBranch)
            .Include(t => t.DestinationBranch)
            .Include(t => t.ProductVariant).ThenInclude(v => v.Product)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);

    public async Task<List<InventoryTransfer>> GetByDestinationBranchAsync(Guid branchId, CancellationToken ct = default)
        => await context.InventoryTransfers
            .Include(t => t.OriginBranch)
            .Include(t => t.DestinationBranch)
            .Include(t => t.ProductVariant).ThenInclude(v => v.Product)
            .Where(t => t.DestinationBranchId == branchId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);

    public async Task AddAsync(InventoryTransfer transfer, CancellationToken ct = default)
    {
        await context.InventoryTransfers.AddAsync(transfer, ct);
        await context.SaveChangesAsync(ct);
    }

    public void Update(InventoryTransfer transfer)
    {
        context.InventoryTransfers.Update(transfer);
        context.SaveChanges();
    }
}

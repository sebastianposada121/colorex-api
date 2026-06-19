using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class BranchInventoryRepository(ColorexDbContext context) : IBranchInventoryRepository
{
    public async Task<BranchInventory?> GetByBranchAndVariantAsync(Guid branchId, Guid variantId, CancellationToken ct = default)
        => await context.BranchInventories
            .FirstOrDefaultAsync(bi => bi.BranchId == branchId && bi.ProductVariantId == variantId, ct);

    public async Task AddAsync(BranchInventory inventory, CancellationToken ct = default)
    {
        await context.BranchInventories.AddAsync(inventory, ct);
        await context.SaveChangesAsync(ct);
    }

    public void Update(BranchInventory inventory)
    {
        context.BranchInventories.Update(inventory);
        context.SaveChanges();
    }
}

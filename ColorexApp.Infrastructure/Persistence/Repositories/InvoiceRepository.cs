using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class InvoiceRepository(ColorexDbContext context) : IInvoiceRepository
{
    public async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Invoices
            .Include(i => i.Items).ThenInclude(it => it.ProductVariant).ThenInclude(v => v.Product)
            .FirstOrDefaultAsync(i => i.Id == id, ct);

    public async Task<List<Invoice>> GetByBranchAsync(Guid branchId, CancellationToken ct = default)
        => await context.Invoices
            .Include(i => i.Items).ThenInclude(it => it.ProductVariant).ThenInclude(v => v.Product)
            .Where(i => i.BranchId == branchId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(ct);

    public async Task<List<Invoice>> GetTodayAsync(CancellationToken ct = default)
    {
        var today = DateTime.UtcNow.Date;
        return await context.Invoices
            .Include(i => i.Items).ThenInclude(it => it.ProductVariant).ThenInclude(v => v.Product)
            .Where(i => i.CreatedAt >= today)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Invoice invoice, CancellationToken ct = default)
    {
        await context.Invoices.AddAsync(invoice, ct);
        await context.SaveChangesAsync(ct);
    }
}

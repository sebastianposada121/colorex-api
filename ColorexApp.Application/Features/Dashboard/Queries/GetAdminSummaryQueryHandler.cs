using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Dashboard;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Dashboard.Queries;

internal sealed class GetAdminSummaryQueryHandler(IDbContext context) : IRequestHandler<GetAdminSummaryQuery, AdminSummaryResponse>
{
    public async Task<AdminSummaryResponse> Handle(GetAdminSummaryQuery request, CancellationToken cancellationToken)
    {
        var totalStock = await context.ProductVariants.SumAsync(v => v.Stock, cancellationToken);
        var activeBranches = await context.Branches.CountAsync(b => b.IsActive, cancellationToken);
        var pendingTransfers = await context.InventoryTransfers
            .CountAsync(t => t.Status == "Pending" || t.Status == "InTransit", cancellationToken);

        var topSelling = await context.InvoiceItems
            .GroupBy(i => i.ProductVariant.Sku)
            .Select(g => new { g.Key, Total = g.Sum(i => i.Quantity) })
            .OrderByDescending(x => x.Total)
            .FirstOrDefaultAsync(cancellationToken);

        string topName = "N/A", topSku = "N/A";
        int topQty = 0;

        if (topSelling is not null)
        {
            topSku = topSelling.Key;
            topQty = topSelling.Total;
            var variant = await context.ProductVariants
                .Include(v => v.Product)
                .FirstAsync(v => v.Sku == topSelling.Key, cancellationToken);
            topName = variant.Product.Name;
        }

        return new AdminSummaryResponse(totalStock, activeBranches, pendingTransfers, topName, topSku, topQty);
    }
}

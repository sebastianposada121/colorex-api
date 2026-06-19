using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Dashboard;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Dashboard.Queries;

internal sealed class GetBranchSummaryQueryHandler(IDbContext context) : IRequestHandler<GetBranchSummaryQuery, BranchSummaryResponse>
{
    public async Task<BranchSummaryResponse> Handle(GetBranchSummaryQuery request, CancellationToken cancellationToken)
    {
        var branch = await context.Branches
            .FirstOrDefaultAsync(b => b.Id == request.BranchId, cancellationToken);

        var branchName = branch?.Name ?? "Unknown";
        var totalStock = await context.BranchInventories
            .Where(bi => bi.BranchId == request.BranchId)
            .SumAsync(bi => bi.QuantityAvailable, cancellationToken);

        var pendingTransfers = await context.InventoryTransfers
            .CountAsync(t => t.DestinationBranchId == request.BranchId
                && (t.Status == "Pending" || t.Status == "InTransit"), cancellationToken);

        var today = DateTime.UtcNow.Date;
        var todaySales = await context.Invoices
            .Where(i => i.BranchId == request.BranchId && i.CreatedAt >= today)
            .CountAsync(cancellationToken);

        var todayRevenue = await context.Invoices
            .Where(i => i.BranchId == request.BranchId && i.CreatedAt >= today)
            .SumAsync(i => i.Total, cancellationToken);

        return new BranchSummaryResponse(branchName, totalStock, pendingTransfers, todaySales, todayRevenue);
    }
}

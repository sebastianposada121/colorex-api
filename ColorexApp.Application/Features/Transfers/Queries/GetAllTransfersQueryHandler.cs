using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Transfers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Transfers.Queries;

internal sealed class GetAllTransfersQueryHandler(IDbContext context) : IRequestHandler<GetAllTransfersQuery, List<TransferResponse>>
{
    public async Task<List<TransferResponse>> Handle(GetAllTransfersQuery request, CancellationToken cancellationToken)
    {
        return await context.InventoryTransfers
            .Include(t => t.OriginBranch)
            .Include(t => t.DestinationBranch)
            .Include(t => t.ProductVariant)
            .ThenInclude(v => v.Product)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TransferResponse(
                t.Id, t.OriginBranchId, t.OriginBranch.Name,
                t.DestinationBranchId, t.DestinationBranch.Name,
                t.ProductVariantId, t.ProductVariant.Sku,
                t.ProductVariant.Product.Name, t.ProductVariant.Size,
                t.ProductVariant.Color, t.Quantity, t.ReceivedQuantity,
                t.Status, t.CreatedAt, t.ReceivedAt
            ))
            .ToListAsync(cancellationToken);
    }
}

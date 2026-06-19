using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Transfers;
using ColorexApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Transfers.Queries;

internal sealed class GetTransferByIdQueryHandler(IDbContext context) : IRequestHandler<GetTransferByIdQuery, TransferResponse?>
{
    public async Task<TransferResponse?> Handle(GetTransferByIdQuery request, CancellationToken cancellationToken)
    {
        var transfer = await context.InventoryTransfers
            .Include(t => t.OriginBranch)
            .Include(t => t.DestinationBranch)
            .Include(t => t.ProductVariant).ThenInclude(v => v.Product)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (transfer is null)
            throw new NotFoundException(nameof(Domain.Entities.InventoryTransfer), request.Id);

        return new TransferResponse(
            transfer.Id, transfer.OriginBranchId, transfer.OriginBranch.Name,
            transfer.DestinationBranchId, transfer.DestinationBranch.Name,
            transfer.ProductVariantId, transfer.ProductVariant.Sku,
            transfer.ProductVariant.Product.Name, transfer.ProductVariant.Size,
            transfer.ProductVariant.Color, transfer.Quantity, transfer.ReceivedQuantity,
            transfer.Status, transfer.CreatedAt, transfer.ReceivedAt
        );
    }
}

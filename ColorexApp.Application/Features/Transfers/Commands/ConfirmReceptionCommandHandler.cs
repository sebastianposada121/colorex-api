using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Transfers.Commands;

internal sealed class ConfirmReceptionCommandHandler(
    IInventoryTransferRepository transferRepository,
    IBranchInventoryRepository branchInventoryRepository) : IRequestHandler<ConfirmReceptionCommand>
{
    public async Task Handle(ConfirmReceptionCommand request, CancellationToken cancellationToken)
    {
        var transfer = await transferRepository.GetByIdAsync(request.TransferId, cancellationToken);
        if (transfer is null)
            throw new NotFoundException(nameof(Domain.Entities.InventoryTransfer), request.TransferId);

        transfer.MarkReceived(request.ReceivedQuantity);
        transferRepository.Update(transfer);

        var branchInventory = await branchInventoryRepository
            .GetByBranchAndVariantAsync(transfer.DestinationBranchId, transfer.ProductVariantId, cancellationToken);

        if (branchInventory is null)
        {
            branchInventory = Domain.Entities.BranchInventory.Create(transfer.DestinationBranchId, transfer.ProductVariantId, request.ReceivedQuantity);
            await branchInventoryRepository.AddAsync(branchInventory, cancellationToken);
        }
        else
        {
            branchInventory.IncreaseQuantity(request.ReceivedQuantity);
            branchInventoryRepository.Update(branchInventory);
        }
    }
}

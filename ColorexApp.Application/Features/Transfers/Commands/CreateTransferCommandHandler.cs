using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Transfers.Commands;

internal sealed class CreateTransferCommandHandler(
    IInventoryTransferRepository transferRepository,
    IProductRepository productRepository) : IRequestHandler<CreateTransferCommand, Guid>
{
    public async Task<Guid> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var variant = await productRepository.GetVariantByIdAsync(request.ProductVariantId, cancellationToken);
        if (variant is null)
            throw new NotFoundException(nameof(ProductVariant), request.ProductVariantId);

        if (variant.Stock < request.Quantity)
            throw new InsufficientStockException(variant.Sku, request.Quantity, variant.Stock);

        variant.DecreaseStock(request.Quantity);
        productRepository.UpdateVariant(variant);

        var transfer = InventoryTransfer.Create(request.OriginBranchId, request.DestinationBranchId, request.ProductVariantId, request.Quantity);
        await transferRepository.AddAsync(transfer, cancellationToken);
        return transfer.Id;
    }
}

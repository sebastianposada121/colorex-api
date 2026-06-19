using ColorexApp.Domain.Enums;

namespace ColorexApp.Domain.Entities;

public class InventoryTransfer
{
    public Guid Id { get; private set; }
    public Guid OriginBranchId { get; private set; }
    public Guid DestinationBranchId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public int Quantity { get; private set; }
    public int ReceivedQuantity { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? ReceivedAt { get; private set; }
    public Branch OriginBranch { get; private set; } = null!;
    public Branch DestinationBranch { get; private set; } = null!;
    public ProductVariant ProductVariant { get; private set; } = null!;

    private InventoryTransfer() { }

    private InventoryTransfer(Guid id, Guid originBranchId, Guid destinationBranchId, Guid productVariantId, int quantity)
    {
        Id = id;
        OriginBranchId = originBranchId;
        DestinationBranchId = destinationBranchId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        ReceivedQuantity = 0;
        Status = TransferStatus.Pending.ToString();
        CreatedAt = DateTime.UtcNow;
    }

    public static InventoryTransfer Create(Guid originBranchId, Guid destinationBranchId, Guid productVariantId, int quantity)
        => new(Guid.NewGuid(), originBranchId, destinationBranchId, productVariantId, quantity);

    public void MarkInTransit() => Status = TransferStatus.InTransit.ToString();
    public void MarkReceived(int receivedQuantity)
    {
        ReceivedQuantity = receivedQuantity;
        Status = TransferStatus.Received.ToString();
        ReceivedAt = DateTime.UtcNow;
    }
}

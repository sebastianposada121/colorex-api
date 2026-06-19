namespace ColorexApp.Domain.Entities;

public class BranchInventory
{
    public Guid Id { get; private set; }
    public Guid BranchId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public int QuantityAvailable { get; private set; }
    public Branch Branch { get; private set; } = null!;
    public ProductVariant ProductVariant { get; private set; } = null!;

    private BranchInventory() { }

    private BranchInventory(Guid id, Guid branchId, Guid productVariantId, int quantityAvailable)
    {
        Id = id;
        BranchId = branchId;
        ProductVariantId = productVariantId;
        QuantityAvailable = quantityAvailable;
    }

    public static BranchInventory Create(Guid branchId, Guid productVariantId, int initialQuantity)
        => new(Guid.NewGuid(), branchId, productVariantId, initialQuantity);

    public void IncreaseQuantity(int quantity) => QuantityAvailable += quantity;
    public void DecreaseQuantity(int quantity) => QuantityAvailable -= quantity;
}

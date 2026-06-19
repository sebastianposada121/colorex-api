namespace ColorexApp.Domain.Entities;

public class InvoiceItem
{
    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal { get; private set; }
    public Invoice Invoice { get; private set; } = null!;
    public ProductVariant ProductVariant { get; private set; } = null!;

    private InvoiceItem() { }

    private InvoiceItem(Guid id, Guid invoiceId, Guid productVariantId, int quantity, decimal unitPrice, decimal subtotal)
    {
        Id = id;
        InvoiceId = invoiceId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = subtotal;
    }

    public static InvoiceItem Create(Guid invoiceId, Guid productVariantId, int quantity, decimal unitPrice)
        => new(Guid.NewGuid(), invoiceId, productVariantId, quantity, unitPrice, quantity * unitPrice);
}

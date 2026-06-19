namespace ColorexApp.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; }
    public Guid BranchId { get; private set; }
    public Guid UserId { get; private set; }
    public string PaymentMethod { get; private set; } = string.Empty;
    public string DianPrefix { get; private set; } = string.Empty;
    public decimal Subtotal { get; private set; }
    public decimal Tax { get; private set; }
    public decimal Total { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Branch Branch { get; private set; } = null!;
    public User User { get; private set; } = null!;
    public ICollection<InvoiceItem> Items { get; private set; } = [];

    private Invoice() { }

    private Invoice(Guid id, Guid branchId, Guid userId, string paymentMethod, string dianPrefix,
        decimal subtotal, decimal tax, decimal total)
    {
        Id = id;
        BranchId = branchId;
        UserId = userId;
        PaymentMethod = paymentMethod;
        DianPrefix = dianPrefix;
        Subtotal = subtotal;
        Tax = tax;
        Total = total;
        CreatedAt = DateTime.UtcNow;
    }

    public static Invoice Create(Guid branchId, Guid userId, string paymentMethod, string dianPrefix,
        decimal subtotal, decimal tax, decimal total)
        => new(Guid.NewGuid(), branchId, userId, paymentMethod, dianPrefix, subtotal, tax, total);
}

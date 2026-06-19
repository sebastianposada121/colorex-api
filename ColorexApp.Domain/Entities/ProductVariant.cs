namespace ColorexApp.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Sku { get; private set; } = string.Empty;
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public string ColorHex { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public Product Product { get; private set; } = null!;

    private ProductVariant() { }

    private ProductVariant(Guid id, Guid productId, string size, string color, string colorHex, int stock)
    {
        Id = id;
        ProductId = productId;
        Size = size;
        Color = color;
        ColorHex = colorHex;
        Stock = stock;
    }

    public static ProductVariant Create(Guid productId, string size, string color, string colorHex, int stock)
        => new(Guid.NewGuid(), productId, size, color, colorHex, stock);

    public void GenerateSku(string baseReference)
        => Sku = $"{baseReference}-{Color.ToUpper().Replace(" ", "")}-{Size.ToUpper()}";

    public void UpdateStock(int newStock) => Stock = newStock;
    public void DecreaseStock(int quantity) => Stock -= quantity;
    public void IncreaseStock(int quantity) => Stock += quantity;
}

namespace ColorexApp.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string BaseReference { get; private set; } = string.Empty;
    public decimal BasePrice { get; private set; }
    public decimal Cost { get; private set; }
    public bool IsActive { get; private set; }
    public ICollection<ProductVariant> Variants { get; private set; } = [];

    private Product() { }

    private Product(Guid id, string name, string baseReference, decimal basePrice, decimal cost)
    {
        Id = id;
        Name = name;
        BaseReference = baseReference;
        BasePrice = basePrice;
        Cost = cost;
        IsActive = true;
    }

    public static Product Create(string name, string baseReference, decimal basePrice, decimal cost)
        => new(Guid.NewGuid(), name, baseReference, basePrice, cost);

    public void Update(string name, decimal basePrice, decimal cost)
    {
        Name = name;
        BasePrice = basePrice;
        Cost = cost;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public ProductVariant AddVariant(string size, string color, string colorHex, int stock)
    {
        var variant = ProductVariant.Create(Id, size, color, colorHex, stock);
        variant.GenerateSku(BaseReference);
        Variants.Add(variant);
        return variant;
    }
}

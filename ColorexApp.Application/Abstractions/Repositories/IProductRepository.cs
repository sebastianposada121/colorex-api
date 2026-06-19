using ColorexApp.Domain.Entities;

namespace ColorexApp.Application.Abstractions.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Product>> GetAllAsync(CancellationToken ct = default);
    Task<ProductVariant?> GetVariantBySkuAsync(string sku, CancellationToken ct = default);
    Task<ProductVariant?> GetVariantByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Product product, CancellationToken ct = default);
    void Update(Product product);
    void UpdateVariant(ProductVariant variant);
}

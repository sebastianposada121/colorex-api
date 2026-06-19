using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class ProductRepository(ColorexDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Products.Include(p => p.Variants).FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<List<Product>> GetAllAsync(CancellationToken ct = default)
        => await context.Products.Include(p => p.Variants).Where(p => p.IsActive).ToListAsync(ct);

    public async Task<ProductVariant?> GetVariantBySkuAsync(string sku, CancellationToken ct = default)
        => await context.ProductVariants.Include(v => v.Product).FirstOrDefaultAsync(v => v.Sku == sku, ct);

    public async Task<ProductVariant?> GetVariantByIdAsync(Guid id, CancellationToken ct = default)
        => await context.ProductVariants.Include(v => v.Product).FirstOrDefaultAsync(v => v.Id == id, ct);

    public async Task AddAsync(Product product, CancellationToken ct = default)
    {
        await context.Products.AddAsync(product, ct);
        await context.SaveChangesAsync(ct);
    }

    public void Update(Product product)
    {
        context.Products.Update(product);
        context.SaveChanges();
    }

    public void UpdateVariant(ProductVariant variant)
    {
        context.ProductVariants.Update(variant);
        context.SaveChanges();
    }
}

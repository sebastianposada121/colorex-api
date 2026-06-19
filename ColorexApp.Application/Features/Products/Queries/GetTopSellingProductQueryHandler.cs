using ColorexApp.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Products.Queries;

internal sealed class GetTopSellingProductQueryHandler(IDbContext context) : IRequestHandler<GetTopSellingProductQuery, TopSellingProductResponse?>
{
    public async Task<TopSellingProductResponse?> Handle(GetTopSellingProductQuery request, CancellationToken cancellationToken)
    {
        var topSelling = await context.InvoiceItems
            .GroupBy(i => i.ProductVariant.Sku)
            .Select(g => new { Sku = g.Key, TotalSold = g.Sum(i => i.Quantity) })
            .OrderByDescending(x => x.TotalSold)
            .FirstOrDefaultAsync(cancellationToken);

        if (topSelling is null) return null;

        var variant = await context.ProductVariants
            .Include(v => v.Product)
            .FirstAsync(v => v.Sku == topSelling.Sku, cancellationToken);

        return new TopSellingProductResponse(variant.Product.Name, topSelling.Sku, topSelling.TotalSold);
    }
}

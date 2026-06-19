using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Products.Queries;

internal sealed class GetAllProductsQueryHandler(IDbContext context) : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
{
    public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await context.Products
            .Include(p => p.Variants)
            .Where(p => p.IsActive)
            .Select(p => new ProductResponse(
                p.Id, p.Name, p.BaseReference, p.BasePrice, p.Cost, p.IsActive,
                p.Variants.Select(v => new ProductVariantResponse(
                    v.Id, v.ProductId, v.Sku, v.Size, v.Color, v.ColorHex, v.Stock
                )).ToList()
            ))
            .ToListAsync(cancellationToken);
    }
}

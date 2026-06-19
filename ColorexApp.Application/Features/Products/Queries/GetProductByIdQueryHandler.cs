using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Products;
using ColorexApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Products.Queries;

internal sealed class GetProductByIdQueryHandler(IDbContext context) : IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        return new ProductResponse(
            product.Id, product.Name, product.BaseReference, product.BasePrice, product.Cost, product.IsActive,
            product.Variants.Select(v => new ProductVariantResponse(
                v.Id, v.ProductId, v.Sku, v.Size, v.Color, v.ColorHex, v.Stock
            )).ToList()
        );
    }
}

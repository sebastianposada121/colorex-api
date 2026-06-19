using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Invoices.Queries;

internal sealed class GetTodayInvoicesQueryHandler(IDbContext context) : IRequestHandler<GetTodayInvoicesQuery, List<InvoiceResponse>>
{
    public async Task<List<InvoiceResponse>> Handle(GetTodayInvoicesQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        return await context.Invoices
            .Include(i => i.Items).ThenInclude(i => i.ProductVariant).ThenInclude(v => v.Product)
            .Where(i => i.CreatedAt >= today)
            .OrderByDescending(i => i.CreatedAt)
            .Select(i => new InvoiceResponse(
                i.Id, i.BranchId, i.UserId, i.PaymentMethod, i.DianPrefix,
                i.Subtotal, i.Tax, i.Total, i.CreatedAt,
                i.Items.Select(it => new InvoiceItemResponse(
                    it.Id, it.ProductVariantId, it.ProductVariant.Sku,
                    it.ProductVariant.Product.Name, it.ProductVariant.Size,
                    it.ProductVariant.Color, it.Quantity, it.UnitPrice, it.Subtotal
                )).ToList()
            ))
            .ToListAsync(cancellationToken);
    }
}

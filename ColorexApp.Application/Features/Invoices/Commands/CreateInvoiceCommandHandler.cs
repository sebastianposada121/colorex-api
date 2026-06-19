using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Invoices.Commands;

internal sealed class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IProductRepository productRepository) : IRequestHandler<CreateInvoiceCommand, Guid>
{
    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        decimal subtotal = 0;
        var invoiceItems = new List<InvoiceItem>();

        foreach (var item in request.Items)
        {
            var variant = await productRepository.GetVariantBySkuAsync(item.Sku, cancellationToken);
            if (variant is null)
                throw new NotFoundByKeyException("ProductVariant", "SKU", item.Sku);

            if (variant.Stock < item.Quantity)
                throw new InsufficientStockException(item.Sku, item.Quantity, variant.Stock);

            variant.DecreaseStock(item.Quantity);
            productRepository.UpdateVariant(variant);

            var invoiceItem = InvoiceItem.Create(Guid.Empty, variant.Id, item.Quantity, item.UnitPrice);
            invoiceItems.Add(invoiceItem);
            subtotal += invoiceItem.Subtotal;
        }

        var tax = subtotal * 0.19m;
        var total = subtotal + tax;

        var invoice = Invoice.Create(request.BranchId, request.UserId, request.PaymentMethod,
            request.DianPrefix, subtotal, tax, total);

        invoiceItems.ForEach(i => typeof(InvoiceItem).GetProperty(nameof(InvoiceItem.InvoiceId))?.SetValue(i, invoice.Id));
        typeof(Invoice).GetProperty("Items")?.SetValue(invoice, invoiceItems);

        await invoiceRepository.AddAsync(invoice, cancellationToken);
        return invoice.Id;
    }
}

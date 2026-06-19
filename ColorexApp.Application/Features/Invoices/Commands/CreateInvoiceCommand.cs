using MediatR;

namespace ColorexApp.Application.Features.Invoices.Commands;

public sealed record CreateInvoiceCommand(
    Guid BranchId,
    Guid UserId,
    string PaymentMethod,
    string DianPrefix,
    List<CreateInvoiceItemCommand> Items
) : IRequest<Guid>;

public sealed record CreateInvoiceItemCommand(string Sku, int Quantity, decimal UnitPrice);

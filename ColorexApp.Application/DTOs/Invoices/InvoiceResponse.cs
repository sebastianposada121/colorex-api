namespace ColorexApp.Application.DTOs.Invoices;

public sealed record InvoiceResponse(
    Guid Id,
    Guid BranchId,
    Guid UserId,
    string PaymentMethod,
    string DianPrefix,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    DateTime CreatedAt,
    List<InvoiceItemResponse> Items
);

public sealed record InvoiceItemResponse(
    Guid Id,
    Guid ProductVariantId,
    string Sku,
    string ProductName,
    string Size,
    string Color,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal
);

public sealed record CreateInvoiceRequest(
    string PaymentMethod,
    List<CreateInvoiceItemRequest> Items
);

public sealed record CreateInvoiceItemRequest(
    string Sku,
    int Quantity,
    decimal UnitPrice
);

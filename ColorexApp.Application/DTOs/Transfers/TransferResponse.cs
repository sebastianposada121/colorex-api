namespace ColorexApp.Application.DTOs.Transfers;

public sealed record TransferResponse(
    Guid Id,
    Guid OriginBranchId,
    string OriginBranchName,
    Guid DestinationBranchId,
    string DestinationBranchName,
    Guid ProductVariantId,
    string Sku,
    string ProductName,
    string Size,
    string Color,
    int Quantity,
    int ReceivedQuantity,
    string Status,
    DateTime CreatedAt,
    DateTime? ReceivedAt
);

public sealed record CreateTransferRequest(
    Guid OriginBranchId,
    Guid DestinationBranchId,
    Guid ProductVariantId,
    int Quantity
);

public sealed record ConfirmReceptionRequest(
    int ReceivedQuantity
);

namespace ColorexApp.Domain.Exceptions;

public sealed class InsufficientStockException(string sku, int requested, int available)
    : DomainException($"Insufficient stock for SKU '{sku}'. Requested: {requested}, Available: {available}.");

namespace ColorexApp.Application.DTOs.Products;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string BaseReference,
    decimal BasePrice,
    decimal Cost,
    bool IsActive,
    List<ProductVariantResponse> Variants
);

public sealed record ProductVariantResponse(
    Guid Id,
    Guid ProductId,
    string Sku,
    string Size,
    string Color,
    string ColorHex,
    int Stock
);

public sealed record CreateProductRequest(
    string Name,
    string BaseReference,
    decimal BasePrice,
    decimal Cost,
    List<CreateVariantRequest> Variants
);

public sealed record CreateVariantRequest(
    string Size,
    string Color,
    string ColorHex,
    int Stock
);

public sealed record UpdateProductRequest(
    string Name,
    decimal BasePrice,
    decimal Cost
);

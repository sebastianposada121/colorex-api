using MediatR;

namespace ColorexApp.Application.Features.Products.Commands;

public sealed record CreateProductCommand(
    string Name,
    string BaseReference,
    decimal BasePrice,
    decimal Cost,
    List<CreateVariantCommand> Variants
) : IRequest<Guid>;

public sealed record CreateVariantCommand(string Size, string Color, string ColorHex, int Stock);

using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using MediatR;

namespace ColorexApp.Application.Features.Products.Commands;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, request.BaseReference, request.BasePrice, request.Cost);

        foreach (var variant in request.Variants)
        {
            product.AddVariant(variant.Size, variant.Color, variant.ColorHex, variant.Stock);
        }

        await productRepository.AddAsync(product, cancellationToken);
        return product.Id;
    }
}

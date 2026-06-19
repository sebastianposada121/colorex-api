using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Products.Commands;

internal sealed class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        product.Update(request.Name, request.BasePrice, request.Cost);
        productRepository.Update(product);
    }
}

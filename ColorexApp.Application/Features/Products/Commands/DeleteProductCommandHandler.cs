using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Products.Commands;

internal sealed class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        product.Deactivate();
        productRepository.Update(product);
    }
}

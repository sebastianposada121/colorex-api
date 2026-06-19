using ColorexApp.Application.DTOs.Products;
using MediatR;

namespace ColorexApp.Application.Features.Products.Queries;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;

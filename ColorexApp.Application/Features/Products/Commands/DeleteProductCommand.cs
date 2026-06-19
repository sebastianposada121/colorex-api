using MediatR;

namespace ColorexApp.Application.Features.Products.Commands;

public sealed record DeleteProductCommand(Guid Id) : IRequest;

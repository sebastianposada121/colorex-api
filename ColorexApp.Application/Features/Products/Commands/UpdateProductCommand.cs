using MediatR;

namespace ColorexApp.Application.Features.Products.Commands;

public sealed record UpdateProductCommand(Guid Id, string Name, decimal BasePrice, decimal Cost) : IRequest;

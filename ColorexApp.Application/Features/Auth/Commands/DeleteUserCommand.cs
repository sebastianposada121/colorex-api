using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

public sealed record DeleteUserCommand(Guid Id) : IRequest;

using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

public sealed record LoginCommand(string Email, string Password) : IRequest<string>;

using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

public sealed record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

public sealed record LoginResult(string Token, string Email, string FirstName, string LastName, string Role, Guid BranchId, Guid UserId);

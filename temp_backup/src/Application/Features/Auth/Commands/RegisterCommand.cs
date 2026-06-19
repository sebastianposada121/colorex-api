using ColorexApp.Domain.Enums;
using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    UserRole Role,
    Guid BranchId) : IRequest<Guid>;

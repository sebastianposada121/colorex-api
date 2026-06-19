using ColorexApp.Domain.Enums;
using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

public sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, UserRole Role, Guid BranchId) : IRequest;

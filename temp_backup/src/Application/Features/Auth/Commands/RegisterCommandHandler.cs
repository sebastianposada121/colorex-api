using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

internal sealed class RegisterCommandHandler(IUserRepository userRepository)
    : IRequestHandler<RegisterCommand, Guid>
{
    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            throw new DuplicateEmailException(request.Email);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(
            request.Email,
            passwordHash,
            request.FirstName,
            request.LastName,
            request.Role,
            request.BranchId);

        await userRepository.AddAsync(user, cancellationToken);

        return user.Id;
    }
}

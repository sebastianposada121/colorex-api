using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Exceptions;
using MediatR;

namespace ColorexApp.Application.Features.Auth.Commands;

internal sealed class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        userRepository.Delete(user);
    }
}

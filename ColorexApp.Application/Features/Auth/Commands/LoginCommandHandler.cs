using ColorexApp.Application.Abstractions.Auth;
using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Auth.Commands;

internal sealed class LoginCommandHandler(IDbContext context, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user is null)
            throw new InvalidCredentialsException();
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException();

        var token = jwtProvider.Generate(user);
        return new LoginResult(token, user.Email, user.FirstName, user.LastName, user.Role, user.BranchId, user.Id);
    }
}

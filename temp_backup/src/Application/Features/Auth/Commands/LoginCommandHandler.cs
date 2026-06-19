using ColorexApp.Application.Abstractions.Auth;
using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Auth.Commands;

internal sealed class LoginCommandHandler(IDbContext context, IJwtProvider jwtProvider)
    : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
            throw new InvalidCredentialsException();

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            throw new InvalidCredentialsException();

        return jwtProvider.Generate(user);
    }
}

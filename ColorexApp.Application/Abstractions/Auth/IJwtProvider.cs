using ColorexApp.Domain.Entities;

namespace ColorexApp.Application.Abstractions.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}

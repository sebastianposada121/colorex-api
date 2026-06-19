using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(ColorexDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Update(User user)
    {
        context.Users.Update(user);
        context.SaveChanges();
    }

    public void Delete(User user)
    {
        context.Users.Remove(user);
        context.SaveChanges();
    }
}

using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(ColorexDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
        => await context.Users.AnyAsync(u => u.Email == email, ct);

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await context.Users.AddAsync(user, ct);
        await context.SaveChangesAsync(ct);
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

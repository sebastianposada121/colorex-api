using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Abstractions.Data;

public interface IDbContext
{
    DbSet<User> Users { get; }

    DbSet<T> Set<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

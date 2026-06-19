using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence;

public sealed class ColorexDbContext(DbContextOptions<ColorexDbContext> options) : DbContext(options), IDbContext
{
    // El DbSet se expone internamente si se requiere, o se accede mediante Set<T>()
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas las configuraciones que implementen IEntityTypeConfiguration en este Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColorexDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}

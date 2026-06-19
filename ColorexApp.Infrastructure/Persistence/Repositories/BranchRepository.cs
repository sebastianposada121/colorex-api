using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence.Repositories;

internal sealed class BranchRepository(ColorexDbContext context) : IBranchRepository
{
    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Branches.FirstOrDefaultAsync(b => b.Id == id, ct);

    public async Task<List<Branch>> GetAllAsync(CancellationToken ct = default)
        => await context.Branches.Where(b => b.IsActive).ToListAsync(ct);

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default)
        => await context.Branches.AnyAsync(b => b.Code == code, ct);

    public async Task AddAsync(Branch branch, CancellationToken ct = default)
    {
        await context.Branches.AddAsync(branch, ct);
        await context.SaveChangesAsync(ct);
    }

    public void Update(Branch branch)
    {
        context.Branches.Update(branch);
        context.SaveChanges();
    }
}

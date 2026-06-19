using ColorexApp.Domain.Entities;

namespace ColorexApp.Application.Abstractions.Repositories;

public interface IBranchRepository
{
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Branch>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default);
    Task AddAsync(Branch branch, CancellationToken ct = default);
    void Update(Branch branch);
}

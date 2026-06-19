using ColorexApp.Domain.Entities;

namespace ColorexApp.Application.Abstractions.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Invoice>> GetByBranchAsync(Guid branchId, CancellationToken ct = default);
    Task<List<Invoice>> GetTodayAsync(CancellationToken ct = default);
    Task AddAsync(Invoice invoice, CancellationToken ct = default);
}

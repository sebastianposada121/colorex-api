using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Abstractions.Data;

public interface IDbContext
{
    DbSet<User> Users { get; }
    DbSet<Branch> Branches { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<BranchInventory> BranchInventories { get; }
    DbSet<InventoryTransfer> InventoryTransfers { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<InvoiceItem> InvoiceItems { get; }
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

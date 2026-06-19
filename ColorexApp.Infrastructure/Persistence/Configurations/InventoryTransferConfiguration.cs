using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class InventoryTransferConfiguration : IEntityTypeConfiguration<InventoryTransfer>
{
    public void Configure(EntityTypeBuilder<InventoryTransfer> builder)
    {
        builder.ToTable("inventory_transfers");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Quantity).IsRequired();
        builder.Property(t => t.ReceivedQuantity).IsRequired();
        builder.Property(t => t.Status).HasMaxLength(30).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.ReceivedAt);
        builder.HasOne(t => t.OriginBranch).WithMany().HasForeignKey(t => t.OriginBranchId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.DestinationBranch).WithMany().HasForeignKey(t => t.DestinationBranchId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.ProductVariant).WithMany().HasForeignKey(t => t.ProductVariantId);
    }
}

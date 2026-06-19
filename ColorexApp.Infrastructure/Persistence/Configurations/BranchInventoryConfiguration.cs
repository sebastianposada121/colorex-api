using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class BranchInventoryConfiguration : IEntityTypeConfiguration<BranchInventory>
{
    public void Configure(EntityTypeBuilder<BranchInventory> builder)
    {
        builder.ToTable("branch_inventories");
        builder.HasKey(bi => bi.Id);
        builder.Property(bi => bi.QuantityAvailable).IsRequired();
        builder.HasIndex(bi => new { bi.BranchId, bi.ProductVariantId }).IsUnique();
        builder.HasOne(bi => bi.Branch).WithMany().HasForeignKey(bi => bi.BranchId);
        builder.HasOne(bi => bi.ProductVariant).WithMany().HasForeignKey(bi => bi.ProductVariantId);
    }
}

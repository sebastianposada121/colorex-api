using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("product_variants");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Sku).HasMaxLength(100).IsRequired();
        builder.HasIndex(v => v.Sku).IsUnique();
        builder.Property(v => v.Size).HasMaxLength(10).IsRequired();
        builder.Property(v => v.Color).HasMaxLength(50).IsRequired();
        builder.Property(v => v.ColorHex).HasMaxLength(7).IsRequired();
        builder.Property(v => v.Stock).IsRequired();
        builder.HasOne(v => v.Product).WithMany(p => p.Variants).HasForeignKey(v => v.ProductId);
    }
}

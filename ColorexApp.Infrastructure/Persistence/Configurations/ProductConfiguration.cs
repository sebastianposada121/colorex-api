using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.BaseReference).HasMaxLength(50).IsRequired();
        builder.HasIndex(p => p.BaseReference).IsUnique();
        builder.Property(p => p.BasePrice).HasPrecision(18, 2).IsRequired();
        builder.Property(p => p.Cost).HasPrecision(18, 2).IsRequired();
        builder.Property(p => p.IsActive).IsRequired();
        builder.HasMany(p => p.Variants).WithOne(v => v.Product).HasForeignKey(v => v.ProductId);
    }
}

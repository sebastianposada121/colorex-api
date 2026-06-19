using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("invoice_items");
        builder.HasKey(ii => ii.Id);
        builder.Property(ii => ii.Quantity).IsRequired();
        builder.Property(ii => ii.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(ii => ii.Subtotal).HasPrecision(18, 2).IsRequired();
        builder.HasOne(ii => ii.Invoice).WithMany(i => i.Items).HasForeignKey(ii => ii.InvoiceId);
        builder.HasOne(ii => ii.ProductVariant).WithMany().HasForeignKey(ii => ii.ProductVariantId);
    }
}

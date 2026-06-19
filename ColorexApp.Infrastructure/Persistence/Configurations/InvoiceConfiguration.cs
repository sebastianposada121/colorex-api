using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.PaymentMethod).HasMaxLength(50).IsRequired();
        builder.Property(i => i.DianPrefix).HasMaxLength(20).IsRequired();
        builder.Property(i => i.Subtotal).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.Tax).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.Total).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.CreatedAt).IsRequired();
        builder.HasOne(i => i.Branch).WithMany().HasForeignKey(i => i.BranchId);
        builder.HasOne(i => i.User).WithMany().HasForeignKey(i => i.UserId);
        builder.HasMany(i => i.Items).WithOne(ii => ii.Invoice).HasForeignKey(ii => ii.InvoiceId);
    }
}

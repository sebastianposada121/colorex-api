using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColorexApp.Infrastructure.Persistence.Configurations;

internal sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("branches");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).HasMaxLength(200).IsRequired();
        builder.Property(b => b.Code).HasMaxLength(20).IsRequired();
        builder.HasIndex(b => b.Code).IsUnique();
        builder.Property(b => b.DianResolutionPrefix).HasMaxLength(20).IsRequired();
        builder.Property(b => b.DianRangeStart).HasMaxLength(20).IsRequired();
        builder.Property(b => b.DianRangeEnd).HasMaxLength(20).IsRequired();
        builder.Property(b => b.IsActive).IsRequired();
    }
}

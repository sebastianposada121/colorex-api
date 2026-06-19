namespace ColorexApp.Infrastructure.Persistence.Configurations;
using ColorexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Nombre de la tabla en PostgreSQL
        builder.ToTable("users");

        // Clave primaria
        builder.HasKey(u => u.Id);

        // Propiedades y restricciones
        builder.Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired();

        // Crear un índice único para el correo electrónico
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Role)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(u => u.BranchId)
            .IsRequired();

        // Nota: En una fase posterior, aquí configurarías la relación externa 
        // con la tabla de sedes (Branch) si decides enlazar los IDs estrictamente:
        // builder.HasOne<Branch>().WithMany().HasForeignKey(u => u.BranchId);
    }
}
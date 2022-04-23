using ApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(e => e.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Password)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(e => e.Role)
            .HasMaxLength(250);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.Active)
            .IsRequired();
    }
}

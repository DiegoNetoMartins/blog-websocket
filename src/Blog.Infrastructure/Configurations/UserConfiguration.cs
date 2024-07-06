using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(user => user.Id).HasName("PK_User");

        builder.Property(user => user.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(user => user.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsOne(user => user.Email)
            .Property(email => email.Address)
            .HasColumnName("Email")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsOne(user => user.Password)
            .Property(password => password.Hash)
            .HasColumnName("PasswordHash")
            .HasColumnType("varchar")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(user => user.CreatedAtOnUtc)
           .HasColumnName("CreatedAtOnUtc")
           .IsRequired();

        builder.Property(user => user.UpdatedAtOnUtc)
            .HasColumnName("UpdatedAtOnUtc");
    }
}
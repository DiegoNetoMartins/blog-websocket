using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");
        builder.HasKey(post => post.Id).HasName("PK_Post");

        builder.Property(post => post.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(post => post.Title)
            .HasColumnName("Title")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(post => post.Content)
            .HasColumnName("Content")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(post => post.CreatedAtOnUtc)
            .HasColumnName("CreatedAtOnUtc")
            .IsRequired();

        builder.Property(post => post.UpdatedAtOnUtc)
            .HasColumnName("UpdatedAtOnUtc");

        builder.Property(post => post.UserId)
            .HasColumnName("UserId")
            .IsRequired();

        builder.HasOne(post => post.User)
            .WithMany(user => user.Posts)
            .HasForeignKey(post => post.UserId)
            .HasConstraintName("FK_Post_UserId_User")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class LocalCredentialsConfiguration : IEntityTypeConfiguration<LocalCredential>
{
    public void Configure(EntityTypeBuilder<LocalCredential> builder)
    {
        builder.ToTable("local_credentials");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(x => x.PasswordAlgo)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.PasswordUpdatedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.LocalCredential)
            .HasForeignKey<LocalCredential>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

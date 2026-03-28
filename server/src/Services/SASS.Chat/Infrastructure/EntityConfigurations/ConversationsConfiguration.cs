using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class ConversationsConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("conversations");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id).HasDefaultValueSql(UniqueIdentifierHelper.NewUuidV7);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.LastMessageUpdatedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder
            .HasIndex(x => new { x.UserId, x.CreatedAt, x.Id })
            .HasDatabaseName("ix_conversations_user_id_created_at_id");

        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ConversationFiles)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

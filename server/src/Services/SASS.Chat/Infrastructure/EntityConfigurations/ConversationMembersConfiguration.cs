using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class ConversationMembersConfiguration : IEntityTypeConfiguration<ConversationMember>
{
    public void Configure(EntityTypeBuilder<ConversationMember> builder)
    {
        builder.ToTable("conversation_members");

        builder.HasKey(x => new { x.ConversationId, x.MemberId });

        builder.Property(x => x.JoinedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => new { x.ConversationId, x.IsDeleted })
            .HasDatabaseName("ix_conversation_members_member_id_is_deleted");

        builder
            .HasOne<Conversation>()
            .WithMany(x => x.ConversationMembers)
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

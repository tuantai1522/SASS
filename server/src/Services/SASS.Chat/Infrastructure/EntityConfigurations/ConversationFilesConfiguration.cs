using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class ConversationFilesConfiguration : IEntityTypeConfiguration<ConversationFile>
{
    public void Configure(EntityTypeBuilder<ConversationFile> builder)
    {
        builder.ToTable("conversation_files");

        builder.HasKey(x => new { x.ConversationId, x.FileId });

        builder.Property(x => x.ConversationId)
            .IsRequired();

        builder.Property(x => x.FileId)
            .IsRequired();

        builder.Property(x => x.Active)
            .IsRequired();

    }
}

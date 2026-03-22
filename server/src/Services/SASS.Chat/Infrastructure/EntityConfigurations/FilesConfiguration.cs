using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = SASS.Chat.Domain.AggregatesModel.Files.File;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class FilesConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("files");

        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id).HasDefaultValueSql(UniqueIdentifierHelper.NewUuidV7);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(x => x.UploadStatus)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder
            .HasMany(x => x.ConversationFiles)
            .WithOne(x => x.File)
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

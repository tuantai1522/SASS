using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskType = SASS.Chat.Domain.AggregatesModel.Projects.TaskType;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class TaskTypesConfiguration : IEntityTypeConfiguration<TaskType>
{
    public void Configure(EntityTypeBuilder<TaskType> builder)
    {
        builder.ToTable("task_types");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(x => x.Key)
            .IsUnique();

        builder.Property(x => x.ColorToken)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.IconKey)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.HasIndex(x => x.Order)
            .IsUnique();

        builder.HasData(
            new { Id = new Guid("aec36fdf-8f53-4a01-a34f-d8ae107e6496"), Name = "Feature", Key = "Feature", ColorToken = "info", IconKey = "sparkles", Order = 1 },
            new { Id = new Guid("b6e8b4dd-62b4-4fb7-813f-07e25e035536"), Name = "Bug", Key = "Bug", ColorToken = "danger", IconKey = "bug", Order = 2 },
            new { Id = new Guid("00c83a8f-e287-4343-84de-2d627dad3f41"), Name = "Improvement", Key = "Improvement", ColorToken = "success", IconKey = "trending-up", Order = 3 },
            new { Id = new Guid("bc5b36da-b555-47f7-bf9a-a7d8104f118e"), Name = "Documentation", Key = "Documentation", ColorToken = "neutral", IconKey = "file-text", Order = 4 },
            new { Id = new Guid("bfeb516d-5146-48b1-87cf-2d1c5bacd7fa"), Name = "Task", Key = "Task", ColorToken = "warning", IconKey = "list-todo", Order = 5 }
        );
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectTaskStatus = SASS.Chat.Domain.AggregatesModel.Projects.TaskStatus;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class TaskStatusesConfiguration : IEntityTypeConfiguration<ProjectTaskStatus>
{
    public void Configure(EntityTypeBuilder<ProjectTaskStatus> builder)
    {
        builder.ToTable("task_statuses");

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
            new { Id = new Guid("54a3f5eb-3c1b-4aa6-9154-ee9164e65862"), Name = "Todo", Key = "Todo", ColorToken = "neutral", IconKey = "circle-help", Order = 1  },
            new { Id = new Guid("ce0b6071-db6d-4820-8a69-095322ccbe3d"), Name = "In-Progress", Key = "InProgress", ColorToken = "info", IconKey = "timer", Order = 2 },
            new { Id = new Guid("02B1EE43-E224-495C-A2CB-53684B30BCB2"), Name = "Review", Key = "Review", ColorToken = "warning", IconKey = "message-square-text", Order = 3 },
            new { Id = new Guid("853bac26-1dc9-4662-8658-6864faa1a9ca"), Name = "Done", Key = "Done", ColorToken = "success", IconKey = "check-circle-2", Order = 4 }
        );
    }
}

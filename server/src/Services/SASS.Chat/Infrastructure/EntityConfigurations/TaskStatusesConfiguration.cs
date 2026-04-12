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

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasData(
            new { Id = new Guid("54a3f5eb-3c1b-4aa6-9154-ee9164e65862"), Name = "Todo" },
            new { Id = new Guid("ce0b6071-db6d-4820-8a69-095322ccbe3d"), Name = "InProgress" },
            new { Id = new Guid("853bac26-1dc9-4662-8658-6864faa1a9ca"), Name = "Done" }
        );
    }
}

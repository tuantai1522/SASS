using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = SASS.Chat.Domain.AggregatesModel.Projects.Task;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class TasksConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("tasks");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql(UniqueIdentifierHelper.NewUuidV7);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Code)
            .IsRequired();

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.AssigneeId)
            .IsRequired(false);

        builder.Property(x => x.StatusId)
            .IsRequired();

        builder.Property(x => x.PriorityId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .HasColumnType("date")
            .IsRequired(false);

        builder.Property(x => x.DueDate)
            .HasColumnType("date")
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => new { x.ProjectId, x.IsDeleted, x.CreatedAt, x.Id })
            .HasDatabaseName("ix_tasks_project_id_is_deleted_created_at_id");

        builder.HasIndex(x => new { x.ProjectId, x.Code })
            .IsUnique()
            .HasFilter("is_deleted = false")
            .HasDatabaseName("ux_tasks_project_id_code_active");

        builder.HasIndex(x => x.AssigneeId)
            .HasDatabaseName("ix_tasks_assignee_id");

        builder.HasIndex(x => x.StatusId)
            .HasDatabaseName("ix_tasks_status_id");

        builder.HasIndex(x => x.PriorityId)
            .HasDatabaseName("ix_tasks_priority_id");

        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Assignee)
            .WithMany()
            .HasForeignKey(x => x.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Priority)
            .WithMany()
            .HasForeignKey(x => x.PriorityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

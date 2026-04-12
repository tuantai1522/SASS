using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class TaskPrioritiesConfiguration : IEntityTypeConfiguration<TaskPriority>
{
    public void Configure(EntityTypeBuilder<TaskPriority> builder)
    {
        builder.ToTable("task_priorities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasData(
            new { Id = new Guid("12fff476-3636-4316-a31b-55d8ad9ee545"), Name = "Low" },
            new { Id = new Guid("4a034f3c-dc71-4582-acb9-af1bbca483d1"), Name = "Medium" },
            new { Id = new Guid("9c1bac75-134c-4155-b0a9-663631db4302"), Name = "High" }
        );
    }
}

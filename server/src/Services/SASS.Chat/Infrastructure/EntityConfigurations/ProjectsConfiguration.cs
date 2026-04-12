using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class ProjectsConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql(UniqueIdentifierHelper.NewUuidV7);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.OwnerId)
            .IsRequired();

        builder.Property(x => x.NextTaskSequence)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => new { x.OwnerId, x.IsDeleted, x.Id })
            .HasDatabaseName("ix_projects_owner_id_is_deleted_id");

        builder.HasIndex(x => new { x.OwnerId, x.Code })
            .IsUnique()
            .HasFilter("is_deleted = false")
            .HasDatabaseName("ux_projects_owner_id_code_active");

        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Members)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Tasks)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

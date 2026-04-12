using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SASS.Chat.Infrastructure.EntityConfigurations;

public sealed class ProjectMembersConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.ToTable("project_members");

        builder.HasKey(x => new { x.ProjectId, x.UserId });

        builder.Property(x => x.Role)
            .HasConversion<string>()
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.JoinedAt)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => new { x.UserId, x.IsDeleted })
            .HasDatabaseName("ix_project_members_user_id_is_deleted");

        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

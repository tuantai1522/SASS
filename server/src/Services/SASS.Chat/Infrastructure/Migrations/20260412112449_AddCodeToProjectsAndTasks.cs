using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeToProjectsAndTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "code",
                table: "tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "projects",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "next_task_sequence",
                table: "projects",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql(
                """
                WITH numbered_projects AS (
                    SELECT
                        id,
                        ROW_NUMBER() OVER (PARTITION BY owner_id ORDER BY created_at, id) AS seq
                    FROM projects
                    WHERE is_deleted = false
                )
                UPDATE projects p
                SET code = 'PRJ-' || LPAD(np.seq::text, 4, '0')
                FROM numbered_projects np
                WHERE p.id = np.id
                  AND p.code = '';
                """);

            migrationBuilder.Sql(
                """
                WITH numbered_tasks AS (
                    SELECT
                        id,
                        ROW_NUMBER() OVER (PARTITION BY project_id ORDER BY created_at, id) AS seq
                    FROM tasks
                    WHERE is_deleted = false
                )
                UPDATE tasks t
                SET code = nt.seq
                FROM numbered_tasks nt
                WHERE t.id = nt.id;
                """);

            migrationBuilder.Sql(
                """
                UPDATE projects p
                SET next_task_sequence = COALESCE(max_task.max_code + 1, 1)
                FROM (
                    SELECT project_id, MAX(code) AS max_code
                    FROM tasks
                    WHERE is_deleted = false
                    GROUP BY project_id
                ) AS max_task
                WHERE p.id = max_task.project_id;
                """);

            migrationBuilder.Sql("ALTER TABLE projects ALTER COLUMN code DROP DEFAULT;");
            migrationBuilder.Sql("ALTER TABLE tasks ALTER COLUMN code DROP DEFAULT;");

            migrationBuilder.CreateIndex(
                name: "ux_tasks_project_id_code_active",
                table: "tasks",
                columns: new[] { "project_id", "code" },
                unique: true,
                filter: "is_deleted = false");

            migrationBuilder.CreateIndex(
                name: "ux_projects_owner_id_code_active",
                table: "projects",
                columns: new[] { "owner_id", "code" },
                unique: true,
                filter: "is_deleted = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_tasks_project_id_code_active",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "ux_projects_owner_id_code_active",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "code",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "code",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "next_task_sequence",
                table: "projects");
        }
    }
}

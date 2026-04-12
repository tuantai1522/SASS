using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuidv7()"),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                    table.ForeignKey(
                        name: "fk_projects_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_priorities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_priorities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "task_statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_members",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    joined_at = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_members", x => new { x.project_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_project_members_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_project_members_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuidv7()"),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    assignee_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status_id = table.Column<Guid>(type: "uuid", nullable: false),
                    priority_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_tasks_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tasks_task_priorities_priority_id",
                        column: x => x.priority_id,
                        principalTable: "task_priorities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tasks_task_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "task_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tasks_users_assignee_id",
                        column: x => x.assignee_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "task_priorities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("12fff476-3636-4316-a31b-55d8ad9ee545"), "Low" },
                    { new Guid("4a034f3c-dc71-4582-acb9-af1bbca483d1"), "Medium" },
                    { new Guid("9c1bac75-134c-4155-b0a9-663631db4302"), "High" }
                });

            migrationBuilder.InsertData(
                table: "task_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("54a3f5eb-3c1b-4aa6-9154-ee9164e65862"), "Todo" },
                    { new Guid("853bac26-1dc9-4662-8658-6864faa1a9ca"), "Done" },
                    { new Guid("ce0b6071-db6d-4820-8a69-095322ccbe3d"), "InProgress" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_members_user_id_is_deleted",
                table: "project_members",
                columns: new[] { "user_id", "is_deleted" });

            migrationBuilder.CreateIndex(
                name: "ix_projects_owner_id_is_deleted_id",
                table: "projects",
                columns: new[] { "owner_id", "is_deleted", "id" });

            migrationBuilder.CreateIndex(
                name: "ix_task_priorities_name",
                table: "task_priorities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_statuses_name",
                table: "task_statuses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tasks_assignee_id",
                table: "tasks",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_priority_id",
                table: "tasks",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_project_id_is_deleted_created_at_id",
                table: "tasks",
                columns: new[] { "project_id", "is_deleted", "created_at", "id" });

            migrationBuilder.CreateIndex(
                name: "ix_tasks_status_id",
                table: "tasks",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_members");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "task_priorities");

            migrationBuilder.DropTable(
                name: "task_statuses");
        }
    }
}

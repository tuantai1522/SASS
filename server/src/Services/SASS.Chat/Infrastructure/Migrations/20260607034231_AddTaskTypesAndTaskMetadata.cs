using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskTypesAndTaskMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "type_id",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("aec36fdf-8f53-4a01-a34f-d8ae107e6496"));

            migrationBuilder.AddColumn<string>(
                name: "color_token",
                table: "task_statuses",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "icon_key",
                table: "task_statuses",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "color_token",
                table: "task_priorities",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "icon_key",
                table: "task_priorities",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "task_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    color_token = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    icon_key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_types", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("12fff476-3636-4316-a31b-55d8ad9ee545"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "success", "arrow-down" });

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("4a034f3c-dc71-4582-acb9-af1bbca483d1"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "warning", "arrow-right" });

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("9c1bac75-134c-4155-b0a9-663631db4302"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "danger", "arrow-up" });

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("02b1ee43-e224-495c-a2cb-53684b30bcb2"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "warning", "message-square-text" });

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("54a3f5eb-3c1b-4aa6-9154-ee9164e65862"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "neutral", "circle-help" });

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("853bac26-1dc9-4662-8658-6864faa1a9ca"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "success", "check-circle-2" });

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("ce0b6071-db6d-4820-8a69-095322ccbe3d"),
                columns: new[] { "color_token", "icon_key" },
                values: new object[] { "info", "timer" });

            migrationBuilder.InsertData(
                table: "task_types",
                columns: new[] { "id", "color_token", "icon_key", "name", "order" },
                values: new object[,]
                {
                    { new Guid("00c83a8f-e287-4343-84de-2d627dad3f41"), "success", "trending-up", "Improvement", 3 },
                    { new Guid("aec36fdf-8f53-4a01-a34f-d8ae107e6496"), "info", "sparkles", "Feature", 1 },
                    { new Guid("b6e8b4dd-62b4-4fb7-813f-07e25e035536"), "danger", "bug", "Bug", 2 },
                    { new Guid("bc5b36da-b555-47f7-bf9a-a7d8104f118e"), "neutral", "file-text", "Documentation", 4 },
                    { new Guid("bfeb516d-5146-48b1-87cf-2d1c5bacd7fa"), "warning", "list-todo", "Task", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_tasks_type_id",
                table: "tasks",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_types_name",
                table: "task_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_types_order",
                table: "task_types",
                column: "order",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_tasks_task_types_type_id",
                table: "tasks",
                column: "type_id",
                principalTable: "task_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("""
                ALTER TABLE tasks ALTER COLUMN type_id DROP DEFAULT;
                ALTER TABLE task_statuses ALTER COLUMN color_token DROP DEFAULT;
                ALTER TABLE task_statuses ALTER COLUMN icon_key DROP DEFAULT;
                ALTER TABLE task_priorities ALTER COLUMN color_token DROP DEFAULT;
                ALTER TABLE task_priorities ALTER COLUMN icon_key DROP DEFAULT;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tasks_task_types_type_id",
                table: "tasks");

            migrationBuilder.DropTable(
                name: "task_types");

            migrationBuilder.DropIndex(
                name: "ix_tasks_type_id",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "color_token",
                table: "task_statuses");

            migrationBuilder.DropColumn(
                name: "icon_key",
                table: "task_statuses");

            migrationBuilder.DropColumn(
                name: "color_token",
                table: "task_priorities");

            migrationBuilder.DropColumn(
                name: "icon_key",
                table: "task_priorities");
        }
    }
}

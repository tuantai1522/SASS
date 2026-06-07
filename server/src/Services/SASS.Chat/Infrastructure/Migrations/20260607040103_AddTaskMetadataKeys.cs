using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskMetadataKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "task_types",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "task_statuses",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "task_priorities",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("12fff476-3636-4316-a31b-55d8ad9ee545"),
                column: "key",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("4a034f3c-dc71-4582-acb9-af1bbca483d1"),
                column: "key",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("9c1bac75-134c-4155-b0a9-663631db4302"),
                column: "key",
                value: "High");

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("02b1ee43-e224-495c-a2cb-53684b30bcb2"),
                column: "key",
                value: "Review");

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("54a3f5eb-3c1b-4aa6-9154-ee9164e65862"),
                column: "key",
                value: "Todo");

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("853bac26-1dc9-4662-8658-6864faa1a9ca"),
                column: "key",
                value: "Done");

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("ce0b6071-db6d-4820-8a69-095322ccbe3d"),
                column: "key",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "task_types",
                keyColumn: "id",
                keyValue: new Guid("00c83a8f-e287-4343-84de-2d627dad3f41"),
                column: "key",
                value: "Improvement");

            migrationBuilder.UpdateData(
                table: "task_types",
                keyColumn: "id",
                keyValue: new Guid("aec36fdf-8f53-4a01-a34f-d8ae107e6496"),
                column: "key",
                value: "Feature");

            migrationBuilder.UpdateData(
                table: "task_types",
                keyColumn: "id",
                keyValue: new Guid("b6e8b4dd-62b4-4fb7-813f-07e25e035536"),
                column: "key",
                value: "Bug");

            migrationBuilder.UpdateData(
                table: "task_types",
                keyColumn: "id",
                keyValue: new Guid("bc5b36da-b555-47f7-bf9a-a7d8104f118e"),
                column: "key",
                value: "Documentation");

            migrationBuilder.UpdateData(
                table: "task_types",
                keyColumn: "id",
                keyValue: new Guid("bfeb516d-5146-48b1-87cf-2d1c5bacd7fa"),
                column: "key",
                value: "Task");

            migrationBuilder.CreateIndex(
                name: "ix_task_types_key",
                table: "task_types",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_statuses_key",
                table: "task_statuses",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_priorities_key",
                table: "task_priorities",
                column: "key",
                unique: true);

            migrationBuilder.Sql("""
                ALTER TABLE task_types ALTER COLUMN key DROP DEFAULT;
                ALTER TABLE task_statuses ALTER COLUMN key DROP DEFAULT;
                ALTER TABLE task_priorities ALTER COLUMN key DROP DEFAULT;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_task_types_key",
                table: "task_types");

            migrationBuilder.DropIndex(
                name: "ix_task_statuses_key",
                table: "task_statuses");

            migrationBuilder.DropIndex(
                name: "ix_task_priorities_key",
                table: "task_priorities");

            migrationBuilder.DropColumn(
                name: "key",
                table: "task_types");

            migrationBuilder.DropColumn(
                name: "key",
                table: "task_statuses");

            migrationBuilder.DropColumn(
                name: "key",
                table: "task_priorities");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnOrderInTaskMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tasks",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuidv7()");

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "task_statuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "task_priorities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("12fff476-3636-4316-a31b-55d8ad9ee545"),
                column: "order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("4a034f3c-dc71-4582-acb9-af1bbca483d1"),
                column: "order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "task_priorities",
                keyColumn: "id",
                keyValue: new Guid("9c1bac75-134c-4155-b0a9-663631db4302"),
                column: "order",
                value: 3);

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("54a3f5eb-3c1b-4aa6-9154-ee9164e65862"),
                column: "order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("853bac26-1dc9-4662-8658-6864faa1a9ca"),
                column: "order",
                value: 4);

            migrationBuilder.UpdateData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("ce0b6071-db6d-4820-8a69-095322ccbe3d"),
                column: "order",
                value: 2);

            migrationBuilder.InsertData(
                table: "task_statuses",
                columns: new[] { "id", "name", "order" },
                values: new object[] { new Guid("02b1ee43-e224-495c-a2cb-53684b30bcb2"), "Review", 3 });

            migrationBuilder.CreateIndex(
                name: "ix_task_statuses_order",
                table: "task_statuses",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_priorities_order",
                table: "task_priorities",
                column: "order",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_task_statuses_order",
                table: "task_statuses");

            migrationBuilder.DropIndex(
                name: "ix_task_priorities_order",
                table: "task_priorities");

            migrationBuilder.DeleteData(
                table: "task_statuses",
                keyColumn: "id",
                keyValue: new Guid("02b1ee43-e224-495c-a2cb-53684b30bcb2"));

            migrationBuilder.DropColumn(
                name: "order",
                table: "task_statuses");

            migrationBuilder.DropColumn(
                name: "order",
                table: "task_priorities");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tasks",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuidv7()",
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}

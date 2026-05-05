using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConversationMembersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuidv7()");

            migrationBuilder.CreateTable(
                name: "conversation_members",
                columns: table => new
                {
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_owner = table.Column<bool>(type: "boolean", nullable: false),
                    joined_at = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversation_members", x => new { x.conversation_id, x.member_id });
                    table.ForeignKey(
                        name: "fk_conversation_members_conversations_conversation_id",
                        column: x => x.conversation_id,
                        principalTable: "conversations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_conversation_members_users_member_id",
                        column: x => x.member_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_conversation_members_member_id",
                table: "conversation_members",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversation_members_member_id_is_deleted",
                table: "conversation_members",
                columns: new[] { "conversation_id", "is_deleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conversation_members");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuidv7()",
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SASS.Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConversationsCursorIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_conversations_user_id",
                table: "conversations");

            migrationBuilder.CreateIndex(
                name: "ix_conversations_user_id_created_at_id",
                table: "conversations",
                columns: new[] { "user_id", "created_at", "id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_conversations_user_id_created_at_id",
                table: "conversations");

            migrationBuilder.CreateIndex(
                name: "ix_conversations_user_id",
                table: "conversations",
                column: "user_id");
        }
    }
}

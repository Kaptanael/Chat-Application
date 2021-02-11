using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class RemoveMessageTypeFromMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

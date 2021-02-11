using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class AddMessageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Message",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AddColumn<int>(
                name: "MessageType",
                table: "Message",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageType",
                table: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Message",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024);
        }
    }
}

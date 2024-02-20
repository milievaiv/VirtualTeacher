using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualTeacher.Migrations
{
    public partial class HasProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasProfileImage",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasProfileImage",
                table: "Users");
        }
    }
}

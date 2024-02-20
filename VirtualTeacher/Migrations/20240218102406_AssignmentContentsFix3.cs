using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualTeacher.Migrations
{
    public partial class AssignmentContentsFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "AssignmentContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "AssignmentContents");
        }
    }
}

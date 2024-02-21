using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualTeacher.Migrations
{
    public partial class RemoveAssignmentContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Assignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

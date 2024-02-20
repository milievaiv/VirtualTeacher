using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualTeacher.Migrations
{
    public partial class AssignmentContentsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentContents_Lectures_AssignmentId",
                table: "AssignmentContents");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentContents_Assignments_AssignmentId",
                table: "AssignmentContents",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentContents_Assignments_AssignmentId",
                table: "AssignmentContents");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentContents_Lectures_AssignmentId",
                table: "AssignmentContents",
                column: "AssignmentId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

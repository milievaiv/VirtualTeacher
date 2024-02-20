using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualTeacher.Migrations
{
    public partial class AssignmentContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentContents_Lectures_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentContents_AssignmentId",
                table: "AssignmentContents",
                column: "AssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentContents");
        }
    }
}

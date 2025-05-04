using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class GradeConfigurationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CourseId1",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseId1",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Grades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "Grades",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseId1",
                table: "Grades",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CourseId1",
                table: "Grades",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}

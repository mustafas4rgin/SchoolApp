using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedGradesColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_ScholarshipApplications_ScholarshipApplicationId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_ScholarshipApplicationId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ScholarshipApplicationId",
                table: "Grades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScholarshipApplicationId",
                table: "Grades",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ScholarshipApplicationId",
                table: "Grades",
                column: "ScholarshipApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_ScholarshipApplications_ScholarshipApplicationId",
                table: "Grades",
                column: "ScholarshipApplicationId",
                principalTable: "ScholarshipApplications",
                principalColumn: "Id");
        }
    }
}

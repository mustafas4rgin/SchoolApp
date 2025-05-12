using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Term",
                table: "TuitionPayments");

            migrationBuilder.AddColumn<int>(
                name: "TermType",
                table: "TuitionPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "TuitionPayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TermType",
                table: "TuitionPayments");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "TuitionPayments");

            migrationBuilder.AddColumn<string>(
                name: "Term",
                table: "TuitionPayments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

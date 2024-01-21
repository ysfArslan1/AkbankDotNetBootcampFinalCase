using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeposited",
                table: "ExpencePayments");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeposited",
                table: "ExpenceResponds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverIban",
                table: "ExpencePayments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "ExpencePayments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeposited",
                table: "ExpenceResponds");

            migrationBuilder.DropColumn(
                name: "ReceiverIban",
                table: "ExpencePayments");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "ExpencePayments");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeposited",
                table: "ExpencePayments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

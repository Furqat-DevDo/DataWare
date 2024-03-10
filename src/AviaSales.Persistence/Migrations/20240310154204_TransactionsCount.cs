using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviaSales.Persistence.Migrations
{
    public partial class TransactionsCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Details_HasTransaction",
                table: "Flights",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Details_TransactionsCount",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details_HasTransaction",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Details_TransactionsCount",
                table: "Flights");
        }
    }
}

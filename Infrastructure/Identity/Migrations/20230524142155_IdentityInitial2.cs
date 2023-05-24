using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IdentityInitial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "TransactionTypes");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "TransactionTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

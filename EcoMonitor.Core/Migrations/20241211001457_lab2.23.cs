using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Core.Migrations
{
    /// <inheritdoc />
    public partial class lab223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAir",
                table: "Calculations",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAir",
                table: "Calculations");
        }
    }
}

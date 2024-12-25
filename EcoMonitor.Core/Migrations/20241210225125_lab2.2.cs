using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Core.Migrations
{
    /// <inheritdoc />
    public partial class lab22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcType",
                table: "Calculations");

            migrationBuilder.AddColumn<bool>(
                name: "isAirPollution",
                table: "Pollutions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAirPollution",
                table: "Pollutions");

            migrationBuilder.AddColumn<int>(
                name: "CalcType",
                table: "Calculations",
                type: "int",
                nullable: true);
        }
    }
}

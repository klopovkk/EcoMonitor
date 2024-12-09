using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Core.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DangerRate",
                table: "Factories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DangerRate",
                table: "Factories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Core.Migrations
{
    /// <inheritdoc />
    public partial class lab2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TaxRateAw",
                table: "Pollutions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TaxRateP",
                table: "Pollutions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalcType",
                table: "Calculations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Tax",
                table: "Calculations",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RadioCreations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactoryId = table.Column<int>(type: "int", nullable: false),
                    OnElectricity = table.Column<float>(type: "real", nullable: true),
                    C1ns = table.Column<float>(type: "real", nullable: true),
                    C1v = table.Column<float>(type: "real", nullable: true),
                    C2ns = table.Column<float>(type: "real", nullable: true),
                    C2v = table.Column<float>(type: "real", nullable: true),
                    V1ns = table.Column<float>(type: "real", nullable: true),
                    V1v = table.Column<float>(type: "real", nullable: true),
                    V2ns = table.Column<float>(type: "real", nullable: true),
                    V2v = table.Column<float>(type: "real", nullable: true),
                    Tax = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creations_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempStorages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N = table.Column<float>(type: "real", nullable: false),
                    V = table.Column<float>(type: "real", nullable: false),
                    T = table.Column<float>(type: "real", nullable: false),
                    Tax = table.Column<float>(type: "real", nullable: false),
                    FactoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creations_FactoryId",
                table: "Creations",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_FactoryId",
                table: "Storages",
                column: "FactoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creations");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropColumn(
                name: "TaxRateAw",
                table: "Pollutions");

            migrationBuilder.DropColumn(
                name: "TaxRateP",
                table: "Pollutions");

            migrationBuilder.DropColumn(
                name: "CalcType",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Calculations");
        }
    }
}

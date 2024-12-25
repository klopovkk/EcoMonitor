using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMonitor.Core.Migrations
{
    /// <inheritdoc />
    public partial class Risk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Risk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactoryId = table.Column<int>(type: "int", nullable: false),
                    PollutionId = table.Column<int>(type: "int", nullable: false),
                    OnElectricity = table.Column<float>(type: "real", nullable: true),
                    Conc = table.Column<float>(type: "real", nullable: true),
                    Rfc = table.Column<float>(type: "real", nullable: true),
                    Hq = table.Column<float>(type: "real", nullable: true),
                    Ladd = table.Column<float>(type: "real", nullable: true),
                    Sf = table.Column<float>(type: "real", nullable: true),
                    Cr = table.Column<float>(type: "real", nullable: true),
                    Date = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risk_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Risk_Pollutions_PollutionId",
                        column: x => x.PollutionId,
                        principalTable: "Pollutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Risk_FactoryId",
                table: "Risk",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_PollutionId",
                table: "Risk",
                column: "PollutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Risk");
        }
    }
}

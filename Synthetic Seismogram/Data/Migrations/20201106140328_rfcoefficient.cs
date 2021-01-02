using Microsoft.EntityFrameworkCore.Migrations;

namespace Synthetic_Seismogram.Data.Migrations
{
    public partial class rfcoefficient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Dt",
                table: "ReflectiveCoefficients",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "AcousticImpedance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TWT = table.Column<float>(nullable: false),
                    AI = table.Column<float>(nullable: false),
                    ReflectiveCoefficientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcousticImpedance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcousticImpedance_ReflectiveCoefficients_ReflectiveCoefficientId",
                        column: x => x.ReflectiveCoefficientId,
                        principalTable: "ReflectiveCoefficients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Synthetic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TWT = table.Column<float>(nullable: false),
                    Sy = table.Column<float>(nullable: false),
                    ReflectiveCoefficientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synthetic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synthetic_ReflectiveCoefficients_ReflectiveCoefficientId",
                        column: x => x.ReflectiveCoefficientId,
                        principalTable: "ReflectiveCoefficients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcousticImpedance_ReflectiveCoefficientId",
                table: "AcousticImpedance",
                column: "ReflectiveCoefficientId");

            migrationBuilder.CreateIndex(
                name: "IX_Synthetic_ReflectiveCoefficientId",
                table: "Synthetic",
                column: "ReflectiveCoefficientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcousticImpedance");

            migrationBuilder.DropTable(
                name: "Synthetic");

            migrationBuilder.DropColumn(
                name: "Dt",
                table: "ReflectiveCoefficients");
        }
    }
}

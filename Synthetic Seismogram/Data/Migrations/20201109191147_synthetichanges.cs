using Microsoft.EntityFrameworkCore.Migrations;

namespace Synthetic_Seismogram.Data.Migrations
{
    public partial class synthetichanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcousticImpedance_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "AcousticImpedance");

            migrationBuilder.DropForeignKey(
                name: "FK_Synthetic_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "Synthetic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Synthetic",
                table: "Synthetic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcousticImpedance",
                table: "AcousticImpedance");

            migrationBuilder.RenameTable(
                name: "Synthetic",
                newName: "Synthetics");

            migrationBuilder.RenameTable(
                name: "AcousticImpedance",
                newName: "AcousticImpedances");

            migrationBuilder.RenameIndex(
                name: "IX_Synthetic_ReflectiveCoefficientId",
                table: "Synthetics",
                newName: "IX_Synthetics_ReflectiveCoefficientId");

            migrationBuilder.RenameIndex(
                name: "IX_AcousticImpedance_ReflectiveCoefficientId",
                table: "AcousticImpedances",
                newName: "IX_AcousticImpedances_ReflectiveCoefficientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Synthetics",
                table: "Synthetics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcousticImpedances",
                table: "AcousticImpedances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcousticImpedances_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "AcousticImpedances",
                column: "ReflectiveCoefficientId",
                principalTable: "ReflectiveCoefficients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Synthetics_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "Synthetics",
                column: "ReflectiveCoefficientId",
                principalTable: "ReflectiveCoefficients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcousticImpedances_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "AcousticImpedances");

            migrationBuilder.DropForeignKey(
                name: "FK_Synthetics_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "Synthetics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Synthetics",
                table: "Synthetics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcousticImpedances",
                table: "AcousticImpedances");

            migrationBuilder.RenameTable(
                name: "Synthetics",
                newName: "Synthetic");

            migrationBuilder.RenameTable(
                name: "AcousticImpedances",
                newName: "AcousticImpedance");

            migrationBuilder.RenameIndex(
                name: "IX_Synthetics_ReflectiveCoefficientId",
                table: "Synthetic",
                newName: "IX_Synthetic_ReflectiveCoefficientId");

            migrationBuilder.RenameIndex(
                name: "IX_AcousticImpedances_ReflectiveCoefficientId",
                table: "AcousticImpedance",
                newName: "IX_AcousticImpedance_ReflectiveCoefficientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Synthetic",
                table: "Synthetic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcousticImpedance",
                table: "AcousticImpedance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcousticImpedance_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "AcousticImpedance",
                column: "ReflectiveCoefficientId",
                principalTable: "ReflectiveCoefficients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Synthetic_ReflectiveCoefficients_ReflectiveCoefficientId",
                table: "Synthetic",
                column: "ReflectiveCoefficientId",
                principalTable: "ReflectiveCoefficients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Synthetic_Seismogram.Data.Migrations
{
    public partial class stringsy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TWT",
                table: "Synthetics",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Sy",
                table: "Synthetics",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TWT",
                table: "Synthetics",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Sy",
                table: "Synthetics",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

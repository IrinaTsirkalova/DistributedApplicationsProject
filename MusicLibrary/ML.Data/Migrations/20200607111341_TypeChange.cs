using Microsoft.EntityFrameworkCore.Migrations;

namespace ML.Data.Migrations
{
    public partial class TypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GenreSongAvgLength",
                table: "Genres",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "GenreSongAvgLength",
                table: "Genres",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBank_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class fixBugsModelsMigrationsFive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoriaCompra",
                table: "ComprasParceladas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoCompra",
                table: "ComprasParceladas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaCompra",
                table: "ComprasParceladas");

            migrationBuilder.DropColumn(
                name: "TipoCompra",
                table: "ComprasParceladas");
        }
    }
}

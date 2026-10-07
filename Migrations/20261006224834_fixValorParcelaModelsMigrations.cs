using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBank_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class fixValorParcelaModelsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorParcela",
                table: "Parcelas");

            migrationBuilder.AddColumn<double>(
                name: "ValorParcela",
                table: "ComprasParceladas",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorParcela",
                table: "ComprasParceladas");

            migrationBuilder.AddColumn<double>(
                name: "ValorParcela",
                table: "Parcelas",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

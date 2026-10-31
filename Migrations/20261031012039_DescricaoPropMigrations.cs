using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBank_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class DescricaoPropMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeProduto",
                table: "ComprasParceladas",
                newName: "Descricao");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Parcelas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Parcelas");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ComprasParceladas",
                newName: "NomeProduto");
        }
    }
}

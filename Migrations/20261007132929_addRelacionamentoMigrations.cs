using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KBank_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class addRelacionamentoMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParcelaId",
                table: "Transacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_ParcelaId",
                table: "Transacoes",
                column: "ParcelaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Parcelas_ParcelaId",
                table: "Transacoes",
                column: "ParcelaId",
                principalTable: "Parcelas",
                principalColumn: "ParcelaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Parcelas_ParcelaId",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_ParcelaId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "ParcelaId",
                table: "Transacoes");
        }
    }
}
